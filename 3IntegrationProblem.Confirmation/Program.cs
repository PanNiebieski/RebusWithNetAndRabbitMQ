using _3IntegrationProblem._0Messages;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using System.Text.Json.Nodes;
using System.Text;
using _3IntegrationProblem.Confirmation;
using System.Text.Json;
using Rebus.Routing.TypeBased;

using var activator = new BuiltinHandlerActivator();
using var http = new HttpClient();

activator.Register((bus, context) => new PhysicalPersonRecordedEventHandler(bus, http));

Configure.With(activator)
    .Logging(l => l.ColoredConsole(minLevel: LogLevel.Warn))
    .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "3IntegrationProblem.Server.Reciver.Confirmation"))

    .Routing(r => r.TypeBased().Map<PhysicalPersonRecordedEvent>("3IntegrationProblem.Server.Reciver.Confirmation")
    .Map<PhysicalPersonApprovedEvent>("3IntegrationProblem.Server.Reciver.Confirmation"))
    .Start();

await activator.Bus.Subscribe<PhysicalPersonRecordedEvent>();
Console.WriteLine("Confirmation is running");
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();

public class PhysicalPersonRecordedEventHandler : IHandleMessages<PhysicalPersonRecordedEvent>
{
    private IBus _bus;
    private HttpClient _http;

    public PhysicalPersonRecordedEventHandler(IBus bus, HttpClient http)
    {
        this._bus = bus;
        this._http = http;
    }

    public async Task Handle(PhysicalPersonRecordedEvent message)
    {
        Console.Write($"PhysicalPerson needs confirmation {message.FirstName} {message.LastName} {message.Pesel}");

        var request = new RequestToExternalApi(message);
        string jsonString = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync("https://localhost:7089/check", content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var res = JsonSerializer.Deserialize<ResponseFromExternalApi>(responseContent, options);

        Console.WriteLine("");
        if (res.Confirmed)
        {
            Console.WriteLine("APPROVED");
            await _bus.Send(new PhysicalPersonApprovedEvent(message.Id));
        }
        else
        {
            Console.WriteLine("REJECTED");
            await _bus.Send(new PhysicalPersonRejectedEvent(message.Id));
        }

    }
}