using Akka.Actor;
using System.Collections.Generic;
using XyzCase.Tweeldata.ApiClient.Models;

namespace XyzCase.Actors
{
    public class DataCollectorResponse
    {
        public DataCollectorResponse(IDictionary<string, Symbol> symbols, IActorRef clientAct)
        {
            Symbols = symbols;
            ClientAct = clientAct;
        }
        public IDictionary<string, Symbol> Symbols { get; }
        public IActorRef ClientAct { get; }
    }

}
