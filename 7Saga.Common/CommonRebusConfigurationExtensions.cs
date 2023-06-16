using Rebus.Config;
using System.Configuration;
using Serilog;
using Rebus.Persistence.FileSystem;
using Rebus.Sagas;
using Rebus.Auditing.Sagas;
using Rebus.Retry.Simple;
using Rebus.Retry.FailFast;
using Rebus.Serialization.Json;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Messages;

namespace _7Saga.Common
{
    public static class CommonRebusConfigurationExtensions
    {
        public static RebusConfigurer ConfigureEndpoint(this RebusConfigurer configurer, EndpointRole role)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RebusDatabase"]?.ConnectionString ?? throw new ConfigurationErrorsException("Could not find 'RebusDatabase' connection string");
            var rabbitMQ = ConfigurationManager.ConnectionStrings["RabbitMQ"]?.ConnectionString ?? throw new ConfigurationErrorsException("Could not find 'RebusDatabase' connection string");
            var filepath = Config.AppSetting("FilePathForSagaAndTimeOuts");

            var filepathSAGAS = Path.Combine(filepath, "FileDatabase_Saga"); 
            var filepathTimeOuts = Path.Combine(filepath, "FileDatabase_TimeOuts");
            var filepathSagaAuditing = Path.Combine(filepath, "FileDatabase_SagaAuditing");

            configurer
                .Logging(l => l.Serilog(Log.Logger))
                .Transport(t =>
                {
                    if (role == EndpointRole.Client)
                    {
                        t.UseRabbitMqAsOneWayClient(rabbitMQ);
                    }
                    else
                    {
                        t.UseRabbitMq(rabbitMQ, Config.AppSetting("QueueName"));
                    }
                })
                //.Subscriptions(s =>
                //{
                //    var subscriptionsTableName = Config.AppSetting("SubscriptionsTableName");

                //    s.StoreInSqlServer(connectionString, subscriptionsTableName, isCentralized: true);
                //})
                .Sagas(s =>
                {
                    //if (role != EndpointRole.SagaHost) return;

                    //var dataTableName = Config.AppSetting("SagaDataTableName");
                    //var indexTableName = Config.AppSetting("SagaIndexTableName");

                    //// store sagas in SQL Server to make them persistent and survive restarts
                    //s.StoreInSqlServer(connectionString, dataTableName, indexTableName);
                    s.UseFilesystem(filepathSAGAS);
                })
                .Timeouts(t =>
                {
                    //if (role == EndpointRole.Client) return;

                    //var tableName = Config.AppSetting("TimeoutsTableName");

                    //// store timeouts in SQL Server to make them persistent and survive restarts
                    //t.StoreInSqlServer(connectionString, tableName);
                    t.UseFileSystem(filepathTimeOuts);
                })
                //.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
                .Options(o => o.EnableSagaAuditing().UseJsonFile(filepathSagaAuditing))
                .Options(o => o.SimpleRetryStrategy(
                    errorQueueAddress: "error",
                    maxDeliveryAttempts: 5,
                    secondLevelRetriesEnabled: false,
                    errorDetailsHeaderMaxLength: int.MaxValue,
                    errorTrackingMaxAgeMinutes: 10)
                );
                //.Options(o => o.FailFastOn(when => when.InnerException != null));

            return configurer;
        }
    }
}





