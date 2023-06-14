using Rebus.Activation;
using Rebus.Config;

using (var act = new BuiltinHandlerActivator())
{
    act.Handle<string>(async message =>
    {
        Console.WriteLine($"Got message : {message}");
    });

    Configure.With(act)
        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "0DemoRebus"))
        .Start();

    Console.WriteLine("Press enter to quit");
    Console.ReadLine();
}