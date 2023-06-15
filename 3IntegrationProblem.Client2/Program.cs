//using _3IntegrationProblem._0Messages;
//using Rebus.Activation;
//using Rebus.Bus;
//using Rebus.Config;
//using Rebus.Handlers;
//using Rebus.Routing.TypeBased;
//using System.Text;
//using static System.Net.WebRequestMethods;

//using (var activator = new BuiltinHandlerActivator())
//{

//    activator.Register(() => new PhysicalPersonApprovedEventHandler());
//    activator.Register(() => new PhysicalPersonRejectedEventHandler());

//    var bus = Configure.With(activator)
//         .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Error))
//        .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "3IntegrationProblem.Server.Reciver.Confirmation"))
//    .Routing(r => r.TypeBased().MapAssemblyOf<PhysicalPersonRecordedEvent>("3IntegrationProblem.Server.Reciver.Confirmation")
//    .MapAssemblyOf<PhysicalPersonApprovedEvent>("3IntegrationProblem.Server.Reciver.Confirmation2"))
//        .Start();

//    await activator.Bus.Subscribe<PhysicalPersonApprovedEvent>();
//    await activator.Bus.Subscribe<PhysicalPersonRejectedEvent>();

//    await Task.Delay(500);

//    Console.WriteLine("T22222222222222222");
//    Console.ReadKey();
//    Console.ReadKey();
//}


//public class PhysicalPersonApprovedEventHandler : IHandleMessages<PhysicalPersonApprovedEvent>
//{

//    public PhysicalPersonApprovedEventHandler()
//    {

//    }

//    public async Task Handle(PhysicalPersonApprovedEvent message)
//    {
//        Console.WriteLine("PhysicalPersonApprovedEvent");

//    }
//}

//public class PhysicalPersonRejectedEventHandler : IHandleMessages<PhysicalPersonRejectedEvent>
//{
//    private IBus _bus;
//    private HttpClient _http;

//    public PhysicalPersonRejectedEventHandler()
//    {

//    }

//    public async Task Handle(PhysicalPersonRejectedEvent message)
//    {

//        Console.WriteLine("PhysicalPersonRejectedEvent");
//    }
//}