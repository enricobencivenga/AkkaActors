using Akka.Actor;
using AkkaActors.Matches.Messages;
using AkkaActors.Standings.Messages;
using Microsoft.AspNetCore.SignalR;

namespace AkkaActors
{
    public class SignalRActor : ReceiveActor
    {
        private readonly IHubContext<AkkaActorsHub> hubContext;

        public SignalRActor(IHubContext<AkkaActorsHub> hubContext)
        {
            this.hubContext = hubContext;

            Receive<MatchChanged>(message => Handle(message));
            Receive<StandingsChanged>(message => Handle(message));
        }

        private void Handle(MatchChanged message)
        {
            hubContext.Clients.All.SendAsync("BroadcastMatchChanged", message);
        }

        private void Handle(StandingsChanged message)
        {
            hubContext.Clients.All.SendAsync("BroadcastStandingsChanged", message);
        }
    }
}