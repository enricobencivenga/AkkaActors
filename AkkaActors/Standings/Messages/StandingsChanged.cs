using System.Collections.Generic;

namespace AkkaActors.Standings.Messages
{
    public class StandingsChanged
    {
        public Dictionary<string, int> Teams { get; set; }
    }
}
