using Rebus.Bus;
using Rebus.Handlers;

public class PhysicalPersonRejectedEventHandler : IHandleMessages<PhysicalPersonRejectedEvent>
{
    private IBus _bus;
    private HttpClient _http;

    public PhysicalPersonRejectedEventHandler()
    {

    }

    public async Task Handle(PhysicalPersonRejectedEvent message)
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("PhysicalPersonRejectedEvent");
        Console.ResetColor();
    }
}