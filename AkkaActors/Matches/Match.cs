namespace AkkaActors.Matches
{
    public class Match
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }
    }
}
