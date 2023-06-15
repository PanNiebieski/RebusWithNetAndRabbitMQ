using _1TooBig._0Messages;
using Rebus.Handlers;

internal class TradeRecordedEventHandler : IHandleMessages<TradeRecordedEvent>
{
    public TradeRecordedEventHandler()
    {
    }

    public async Task Handle(TradeRecordedEvent message)
    {
        Console.WriteLine
            ($"Invocing trade: {message.Id} ({message.Quantity} x {message.Commodity})");
    }
}


internal class UserLoggedEventHandler : IHandleMessages<UserLoggedEvent>
{
    public UserLoggedEventHandler()
    {
    }

    public async Task Handle(UserLoggedEvent message)
    {
        Console.WriteLine
            ($"User was logged: {message.Id} ({message.UserName})");
    }
}

internal class DocumentSavedEventHandler : IHandleMessages<DocumentSavedEvent>
{
    public DocumentSavedEventHandler()
    {
    }

    public async Task Handle(DocumentSavedEvent message)
    {
        Console.WriteLine
            ($"Document was saved: {message.Id} ({message.FileName})");
    }
}