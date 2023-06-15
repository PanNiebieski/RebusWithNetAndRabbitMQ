using _3IntegrationProblem._0Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using System.Text;
using static System.Net.WebRequestMethods;

Console.Title = "Here we gonna send PhysicalPerson records";

using (var activator = new BuiltinHandlerActivator())
{

    activator.Register(() => new PhysicalPersonApprovedEventHandler());
    activator.Register(() => new PhysicalPersonRejectedEventHandler());
    activator.Register(() => new PhysicalPersonCannotCheckEventHandler());

    var bus = Configure.With(activator)
        .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Error))
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "3IntegrationProblem.Server.Reciver.Confirmation"))
        .Routing(r => r.TypeBased()
        .Map<PhysicalPersonRecordedEvent>("3IntegrationProblem.Server.Reciver.Confirmation")
        .Map<PhysicalPersonCannotCheckEvent>("3IntegrationProblem.Server.Reciver.Confirmation")
        .Map<PhysicalPersonApprovedEvent>("3IntegrationProblem.Server.Reciver.Confirmation")
        .Map<PhysicalPersonRejectedEvent>("3IntegrationProblem.Server.Reciver.Confirmation"))
        .Start();

    await activator.Bus.Subscribe<PhysicalPersonApprovedEvent>();
    await activator.Bus.Subscribe<PhysicalPersonRejectedEvent>();
    await activator.Bus.Subscribe<PhysicalPersonCannotCheckEvent>();

    await Task.Delay(500);

    Console.WriteLine("Here we gonna send PhysicalPerson records");
    Console.WriteLine("Press any key to start");
    Console.ReadKey();

    while (true)
    {
        var eve = new PhysicalPersonRecordedEvent(Guid.NewGuid(), "Cezary", "Walenciuk", "1122233456");
        Console.WriteLine("Sending PhysicalPersonRecordedEvent");
        await bus.Publish(eve);
        await Task.Delay(2000);
    }

   

}
