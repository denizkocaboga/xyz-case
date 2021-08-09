using Akka.Actor;
using Akka.DI.Core;
using System.Collections.Generic;
using XyzCase.Messages;

namespace XyzCase.Actors
{
    public class CalculateSupervisorActor : ReceiveActor
    {
        private readonly IDictionary<string, IActorRef> _calculators;

        public CalculateSupervisorActor()
        {
            _calculators = new Dictionary<string, IActorRef>();

            Receive<DataCollectorResponse>(message =>
            {
                foreach (var item in message.Symbols)
                {
                    if (!_calculators.ContainsKey(item.Key))
                        _calculators.Add(item.Key, Context.ActorOf(Context.System.DI().Props<CalculatorActor>()));

                    IActorRef actor = _calculators[item.Key];

                    var calcMessage = new CalculatorMessage(item.Value, message.ClientAct);

                    actor.Tell(calcMessage);
                }
            });
        }
    }

}
