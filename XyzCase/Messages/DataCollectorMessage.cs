using Akka.Actor;
using System.Collections.Generic;

namespace XyzCase.Messages
{
    public class StartMessage { }

    public class DataCollectorMessage
    {
        public DataCollectorMessage(IEnumerable<string> symbols, string interval, IActorRef clientAct)
        {
            Symbols = symbols;
            Interval = interval;
            ClientAct = clientAct;
        }

        public IEnumerable<string> Symbols { get; }
        public string Interval { get; }
        public IActorRef ClientAct { get; }
    }



}
