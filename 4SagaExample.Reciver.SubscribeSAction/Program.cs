using _4SagaExample._0Messages;
using _4SagaExample.Reciver.SubscribeSAction;
using Rebus.Config;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddRebus(
    rebus => rebus
        .Routing(r =>
            r.TypeBased().MapAssemblyOf<Program>("newsletter-queue"))
        .Logging(
               l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Error))
        .Transport(t =>
               t.UseRabbitMq("amqp://guest:guest@localhost:5672", "newsletter-queue"))
        .Routing(r =>
               r.TypeBased().MapAssemblyOf<SubscribeToNewsletter>("newsletter-queue"))
        .Sagas(s =>
               s.UseFilesystem(@"C:\Users\PanNiebieski\source\repos\RebusWithNet\4SagaExample\Saga\"))
        .Timeouts(t =>
               t.UseFileSystem(@"C:\Users\PanNiebieski\source\repos\RebusWithNet\4SagaExample\TimeOuts\"))
);

builder.Services.AutoRegisterHandlersFromAssemblyOf<Program>();

var app = builder.Build();


app.MapGet("/", () => "Saga REBUS");
app.UseHttpsRedirection();
app.Run();
