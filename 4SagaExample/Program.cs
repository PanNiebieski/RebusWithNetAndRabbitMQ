using _4SagaExample._0Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;

using (var activator = new BuiltinHandlerActivator())
{
    Console.Title = "4SagaExample.Start";


    var config = Configure.With(activator)
        .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Error))
        .Transport(t => 
                t.UseRabbitMq("amqp://guest:guest@localhost:5672", "newsletter-queue"))
        //.Routing(r => 
        //        r.TypeBased().MapAssemblyOf<Program>("newsletter-queue"))
        .Sagas(s => 
                s.UseFilesystem(@"C:\Users\PanNiebieski\source\repos\RebusWithNet\4SagaExample\Saga\"))
        .Timeouts(t => 
                t.UseFileSystem(@"C:\Users\PanNiebieski\source\repos\RebusWithNet\4SagaExample\TimeOuts\"));

    var bus = config.Start();

    await bus.Send(new WelcomeEmailSent(Guid.NewGuid(),"c@gmail.com"));

    await Task.Delay(500);

    Console.WriteLine("Error Reciver is waiting for errors");
    Console.ReadKey();


}