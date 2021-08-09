using Akka.Actor;
using XyzCase.Messages;
using XyzCase.Settings;

namespace XyzCase.Actors
{
    public class StartActor : ReceiveActor
    {
        private readonly CaseSettings _settings;
        private readonly IActorRef _getdataActor;

        public StartActor(IActorRef getdataActor)
        {
            _getdataActor = getdataActor;
            Receive<StartMessage>(inputMessage =>
            {
                //IActorRef getDataActor = Context.ActorOf(Props.Create<GetDataActor>());

                var gdMessage = new DataCollectorMessage(_settings.Symbols, _settings.Interval);

                _getdataActor.Tell(gdMessage);
            });
            
        }
    }

}
