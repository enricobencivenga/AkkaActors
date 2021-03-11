using Akka.Actor;
using AkkaActors.Matches.Messages;
using AkkaActors.Standings.Messages;

namespace AkkaActors.Standings.Actors
{
    public class StandingsActor : ReceiveActor, ILogReceive
    {
        private Standings CurrentStanding = new Standings();
        public StandingsActor()
        {
            Receive<MatchChanged>(request => Handle(request));
            Receive<GetStandingsRequest>(request => Handle(request));
        }

        private void Handle(MatchChanged request)
        {
            UpdateStandings(request.Match.Team1, request.Match.Team1Score, request.Match.Team2Score);
            UpdateStandings(request.Match.Team2, request.Match.Team2Score, request.Match.Team1Score);

            Context.ActorSelection(ActorPaths.SignalRActor).Tell(new StandingsChanged { Teams = CurrentStanding.Teams });
        }

        private void Handle(GetStandingsRequest request)
        {
            Context.Sender.Tell(new GetStandingsResponse { Teams = CurrentStanding.Teams });
        }

        private void UpdateStandings(string team, int ownScore, int rivalScore)
        {
            var points = ownScore > rivalScore ? 3 : ownScore < rivalScore ? 0 : 1;
            if (CurrentStanding.Teams.ContainsKey(team))
                CurrentStanding.Teams[team] = points;
            else
                CurrentStanding.Teams.Add(team, points);
        }
    }
}
