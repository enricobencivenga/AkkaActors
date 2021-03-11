using System.Collections.Generic;

namespace AkkaActors.Matches.Messages
{
    public class GetMatchesRequest
    {
    }

    public class GetMatchesResponse
    {
        public List<Match> Matches { get; set; }
    }
}
