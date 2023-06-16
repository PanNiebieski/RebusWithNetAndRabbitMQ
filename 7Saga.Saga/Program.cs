using _7Saga.Common;
using _7Saga.Messages;
using _7Saga.Saga;
using _7Saga.Saga.Messages;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.Sagas;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine(AppInfo.Value);



Log.Logger = new LoggerConfiguration()
    .WriteTo.ColoredConsole
    (outputTemplate: "{Timestamp:HH:mm:ss} {Message}{NewLine}{Exception}")
    .CreateLogger();

using (var activator = new BuiltinHandlerActivator())
{
    activator.Register((bus, context) => new PayoutSaga(bus));

    using var bus = Configure.With(activator)
    .ConfigureEndpoint(EndpointRole.SagaHost)
    .Start();

    await bus.Subscribe<AmountsCalculated>();
    await bus.Subscribe<TaxesCalculated>();
    await bus.Subscribe<PayoutMethodSelected>();

    while (true)
    {
        Console.ReadKey();
    }
}



