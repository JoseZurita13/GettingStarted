using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GettingStarted
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        /*
                        
                        x.UsingAzureServiceBus(configure: (context, cfg) =>
                        {
                            // NOT PRODUCTION CODE - Use Primary Connection String from Shared access policies
                            cfg.Host(connectionString: "Endpoint=sb://poc-masstransit-servicebus.servicebus.windows.net/;SharedAccessKeyName=getting-started;SharedAccessKey=eH4dvEbLYlPX1R6swIICv8fhHcz5sCS/n+ASbMsDsFI=");
                            cfg.ConfigureEndpoints(context);
                        });
                        */



                        x.UsingInMemory((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                        
                        
                        
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
