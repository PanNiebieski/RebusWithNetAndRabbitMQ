using Rebus.Handlers;

public class PhysicalPersonCannotCheckEventHandler : IHandleMessages<PhysicalPersonCannotCheckEvent>
{

    public PhysicalPersonCannotCheckEventHandler()
    {

    }

    public async Task Handle(PhysicalPersonCannotCheckEvent message)
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("PhysicalPersonCannotCheckEvent");
        Console.ResetColor();
    }
}

