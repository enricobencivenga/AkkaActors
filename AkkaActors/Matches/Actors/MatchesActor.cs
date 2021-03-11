using Akka.Actor;
using AkkaActors.Matches.Messages;
using AkkaActors.Standings.Messages;
using System.Collections.Generic;
using System.Linq;

namespace AkkaActors.Matches.Actors
{
    public class MatchesActor : ReceiveActor, ILogReceive
    {
        private List<Match> Matches = new List<Match>();

        public MatchesActor()
        {
            Receive<GetMatchesRequest>(request => Handle(request));
            Receive<MatchCreated>(request => Handle(request));
            Receive<MatchChanged>(request => Handle(request));
        }

        protected override void PreStart()
        {
        }

        private void Handle(GetMatchesRequest request)
        {
            Context.Sender.Tell(new GetMatchesResponse() { Matches = Matches });
        }

        private void Handle(MatchCreated request)
        {
            var matchActor = GetMatchActor(request.Match.Id);
            matchActor.Tell(new MatchCreated() { Match = request.Match });
        }

        private void Handle(MatchChanged request)
        {
            var match = Matches.FirstOrDefault(m => m.Id == request.Match.Id);

            if (match == null)
                Matches.Add(request.Match);
            else
                match = request.Match;

            Context.ActorSelection(ActorPaths.StandingsActor).Tell(request);
            Context.ActorSelection(ActorPaths.SignalRActor).Tell(request);
        }

        private IActorRef GetMatchActor(int matchId)
        {
            var childActor = Context.Child($"{matchId}");

            if (childActor is Nobody)
            {
                childActor = Context.ActorOf(Props.Create(() => new MatchActor()), $"{matchId}");
            }

            return childActor;
        }
    }
}
