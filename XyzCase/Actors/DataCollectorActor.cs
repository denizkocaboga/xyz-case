using Akka.Actor;
using Akka.DI.Core;
using System;
using XyzCase.Messages;
using XyzCase.Tweeldata.ApiClient;

namespace XyzCase.Actors
{
    public class DataCollectorActor : ReceiveActor
    {
        private readonly IApiClient _apiClient;

        private readonly IActorRef _supervisorAct;

        public DataCollectorActor(IApiClient apiClient)
        {
            _apiClient = apiClient;

            _supervisorAct = Context.System.ActorOf(Context.System.DI().Props<CalculateSupervisorActor>(), "calculate-supervisor");

            Receive<DataCollectorMessage>(message =>
            {
                var response = _apiClient.GetPrices(message.Symbols, message.Interval);

                if (response.IsSuccess)
                    _supervisorAct.Tell(new DataCollectorResponse(response.Result, message.ClientAct));
                else
                    message.ClientAct.Tell($"{response.Message} {Environment.NewLine}Detail: {response.Detail}");
            });
        }
    }

}
