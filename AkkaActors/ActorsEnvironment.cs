using Akka.Actor;
using AkkaActors.Matches.Actors;
using AkkaActors.Standings.Actors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AkkaActors
{
    public class ActorsEnvironment
    {
        private readonly ActorSystem system;

        private ActorsEnvironment(ActorSystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            this.system = system;
        }

        public static ActorsEnvironment CreateAndConfigure(IServiceProvider provider)
        {
            var hubContext = provider.GetService<IHubContext<AkkaActorsHub>>();

            var system = ActorSystem.Create("akkaActors");

            system.ActorOf(Props.Create(() => new MatchesActor()), "matches");
            system.ActorOf(Props.Create(() => new StandingsActor()), "standings");
            system.ActorOf(Props.Create(() => new SignalRActor(hubContext)), "signalR");

            return new ActorsEnvironment(system);
        }

        public ActorSelection SelectActor(string path)
        {
            return system.ActorSelection(path);
        }
    }

    public class ActorPaths
    {
        public const string MatchesActor = "/user/matches";
        public const string StandingsActor = "/user/standings";
        public const string SignalRActor = "/user/signalR";

        public const string AllActors = "/user/*";
    }
}