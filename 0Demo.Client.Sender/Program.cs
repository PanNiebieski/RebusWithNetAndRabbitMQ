using Rebus.Activation;
using Rebus.Config;

using (var activator = new BuiltinHandlerActivator())
{
    var bus = Configure.With(activator)
        .Logging(l => l.ColoredConsole())
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "0DemoRebus"))
        .Start();

    await Task.Delay(500);

    while (true)
    {
        Console.Write("Type something >");
        var text = Console.ReadLine();

        if (string.IsNullOrEmpty(text)) break;

        bus.Publish(text).Wait();
    }
}