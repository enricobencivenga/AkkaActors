using System.Collections.Generic;

namespace AkkaActors.Standings.Messages
{
    public class GetStandingsRequest
    {
    }

    public class GetStandingsResponse
    {
        public Dictionary<string, int> Teams { get; set; }
    }
}
