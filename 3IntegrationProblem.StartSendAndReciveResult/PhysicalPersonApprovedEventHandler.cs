using Rebus.Handlers;

public class PhysicalPersonApprovedEventHandler : IHandleMessages<PhysicalPersonApprovedEvent>
{

    public PhysicalPersonApprovedEventHandler()
    {

    }

    public async Task Handle(PhysicalPersonApprovedEvent message)
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("PhysicalPersonApprovedEvent");
        Console.ResetColor();
    }
}

