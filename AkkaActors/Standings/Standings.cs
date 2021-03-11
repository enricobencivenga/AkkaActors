using System.Collections.Generic;

namespace AkkaActors.Standings
{
    public class Standings
    {
        public Dictionary<string, int> Teams { get; set; }

        public Standings()
        {
            Teams = new Dictionary<string, int>();
        }
    }
}
