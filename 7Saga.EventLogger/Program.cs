using _7Saga.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Serilog;
using _7Saga.Common;
using _7Saga.EventLogger;

Console.WriteLine(AppInfo.Value);

Log.Logger = new LoggerConfiguration()
    .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm:ss} {Message}{NewLine}{Exception}")
    .CreateLogger();

using (var activator = new BuiltinHandlerActivator())
{
    activator.Register(() => new EventLogger());

    var bus = Configure.With(activator)
        .ConfigureEndpoint(EndpointRole.Subscriber)
        .Start();

    Task.WaitAll(
        bus.Subscribe<AmountsCalculated>(),
        bus.Subscribe<TaxesCalculated>(),
        bus.Subscribe<PayoutMethodSelected>(),
        bus.Subscribe<PayoutReady>(),
        bus.Subscribe<PayoutNotReady>()
    );

    Console.WriteLine("Press ENTER to quit");
    Console.ReadLine();
}

class EventLogger : IHandleMessages<IEventWithCaseNumber>
{
    static readonly ILogger Logger = Log.ForContext<EventLogger>();

    public async Task Handle(IEventWithCaseNumber message)
    {
        Logger.Information("Got event {EventName} for case {CaseNumber}",
            message.GetType().Name, message.CaseNumber);
    }
}