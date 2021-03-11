using Akka.Actor;
using AkkaActors.Matches.Messages;
using System;

namespace AkkaActors.Matches.Actors
{
    public class MatchActor : ReceiveActor, ILogReceive
    {
        private Match CurrentMatch;

        public MatchActor()
        {
            Receive<MatchCreated>(request => Handle(request));
            Receive<MatchStarted>(request => Handle(request));
            Receive<MatchEnded>(request => Handle(request));
            Receive<MatchScoreChanged>(request => Handle(request));
        }

        protected override void PreStart()
        {
            CurrentMatch = new Match();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"Match actor {Self.Path.Name} stopped");
        }

        private void Handle(MatchCreated request)
        {
            CurrentMatch = request.Match;
        }

        private void Handle(MatchStarted request)
        {
            CurrentMatch.Started = true;
            CurrentMatch.Team1Score = 0;
            CurrentMatch.Team2Score = 0;

            Context.Parent.Tell(new MatchChanged() { Match = CurrentMatch });
        }

        private void Handle(MatchEnded request)
        {
            CurrentMatch.Ended = true;
            SendMatchChangedToParent();

            Context.Stop(Self);
        }

        private void Handle(MatchScoreChanged request)
        {
            CurrentMatch.Team1Score = request.Team1Score;
            CurrentMatch.Team2Score = request.Team2Score;

            SendMatchChangedToParent();
        }

        private void SendMatchChangedToParent()
        {
            Context.Parent.Tell(new MatchChanged() { Match = CurrentMatch });
        }
    }
}
