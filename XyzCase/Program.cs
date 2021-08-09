using Akka.Actor;
using Akka.Configuration;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.IO;
using System.Threading;
using XyzCase.Actors;
using XyzCase.Extensions;
using XyzCase.Messages;
using XyzCase.Settings;
using XyzCase.Tweeldata.ApiClient;
using XyzCase.Tweeldata.ApiClient.Settings;

namespace XyzCase
{
    class Program
    {
        private static ILogger<Program> _logger;
        static void Main(string[] args)
        {
            _logger = LoggerFactory.Create(builder => builder.AddSimpleConsole()).CreateLogger<Program>();

            try
            {
                IServiceProvider provider = ConfigureServices();

                RunActor(provider);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "an error occurred while program initializing!");
            }
            finally
            {
                _logger.LogInformation("application shutting down!");
                Console.ReadLine();
            }
        }

        private static void RunActor(IServiceProvider provider)
        {
            Config config = ConfigurationFactory.ParseString(File.ReadAllText("akka-config.hocon"));
            ActorSystem actorSystem = ActorSystem.Create("system", config).UseServiceProvider(provider);

            IActorRef dcActor = actorSystem.ActorOf(actorSystem.DI().Props<DataCollectorActor>(), "data-collector");
            IActorRef consoleAct = actorSystem.ActorOf(actorSystem.DI().Props<ConsoleActor>(), "console");

            //ConsoleActor.CompletionSource = new TaskCompletionSource<bool>();

            CaseSettings settings = provider.GetService<IOptions<CaseSettings>>().Value;
            var message = new DataCollectorMessage(settings.Symbols, settings.Interval, consoleAct);

            consoleAct.Tell($"Time\t\t{settings.Symbols.Combine()}");
            int counter = 0;
            while (counter++ < 100)
            {
                try
                {
                    dcActor.Tell(message);
                }
                catch (Exception e)
                {
                    consoleAct.Tell(e.Message);
                }

                Thread.Sleep(TimeSpan.FromSeconds(settings.WorkingPeriod));
            }

            //ConsoleActor.CompletionSource.Task.Wait();
        }


        private static IServiceProvider ConfigureServices()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return new ServiceCollection()
                .Configure<TweeldataApiSettings>(options => config.GetSection(nameof(TweeldataApiSettings)).Bind(options))
                .Configure<CaseSettings>(options => config.GetSection(nameof(CaseSettings)).Bind(options))

                .AddSingleton<ConsoleActor>()
                .AddSingleton<CalculateSupervisorActor>()
                .AddScoped<DataCollectorActor>()
                .AddScoped<CalculatorActor>()

                .AddTransient<IApiClient, ApiClient>()
                .AddTransient<IRestClient, RestClient>()
                .BuildServiceProvider();
        }
    }


}
