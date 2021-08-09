using Akka.Actor;
using System.Linq;
using XyzCase.Messages;
using XyzCase.Tweeldata.ApiClient.Models;

namespace XyzCase.Actors
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<CalculatorMessage>(message =>
            {
                Symbol symbol = message.Symbol;

                //decimal avarage = symbol.Prices.Average(p => p.Close);
                decimal avarage = symbol.Prices.Average(p => (p.High + p.Low) / 2);

                var response = new CalculatorResponse(symbol.Meta.Symbol, avarage);

                message.ClientAct.Tell(response);
            });
        }
    }

}
