using Akka.Actor;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using XyzCase.Extensions;
using XyzCase.Settings;

namespace XyzCase.Actors
{
    public class ConsoleActor : ReceiveActor
    {
        //public static TaskCompletionSource<bool> CompletionSource;

        private Dictionary<string, string> avarages;

        private readonly CaseSettings _settings;

        public ConsoleActor(IOptions<CaseSettings> options)
        {
            _settings = options.Value;
            FlushAvarages();

            Receive<CalculatorResponse>(msg =>
            {
                avarages[msg.Symbol] = msg.AvgPrice.ToString("#.000");

                if (avarages.Values.All(p => !p.IsNullOrEmpty()))
                {
                    string message = avarages.OrderBy(p => p.Key).Select(p => p.Value).Combine();
                    Self.Tell($"{DateTime.Now:HH:mm:ss}\t{message}");
                    FlushAvarages();
                    //CompletionSource.SetResult(true);
                }
            });


            Receive<string>(message =>
            {
                Console.WriteLine(message);
            });
        }

        private void FlushAvarages()
        {
            avarages = _settings.Symbols.ToDictionary(key => key, value => default(string));
        }
    }

}
