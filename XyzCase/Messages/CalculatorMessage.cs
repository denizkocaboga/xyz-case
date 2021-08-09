using Akka.Actor;
using XyzCase.Tweeldata.ApiClient.Models;

namespace XyzCase.Messages
{
    public class CalculatorMessage
    {
        public CalculatorMessage(Symbol symbol, IActorRef client)
        {
            Symbol = symbol;
            ClientAct = client;
        }
        public Symbol Symbol { get; }
        public IActorRef ClientAct { get; }
    }



}
