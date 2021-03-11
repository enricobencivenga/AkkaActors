using System.Threading;

namespace AkkaActors.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var matchSimulator = new MatchSimulator();

            // CREATE MATCHES
            matchSimulator.CreateMatch(1, "Team 1-1", "Team 1-2");
            Thread.Sleep(1000);

            matchSimulator.CreateMatch(2, "Team 2-1", "Team 2-2");
            Thread.Sleep(1000);

            // START MATCHES
            matchSimulator.StartMatch(1);
            Thread.Sleep(1000);
            matchSimulator.StartMatch(2);
            Thread.Sleep(1000);

            // CHANGE SCORE FOR MATCHES
            matchSimulator.ChangeScore(1, 1, 0);
            Thread.Sleep(1000);
            matchSimulator.ChangeScore(2, 0, 1);
            Thread.Sleep(1000);
            matchSimulator.ChangeScore(1, 1, 1);
            Thread.Sleep(1000);
            matchSimulator.ChangeScore(1, 1, 2);
            Thread.Sleep(1000);
            matchSimulator.ChangeScore(2, 1, 1);
            Thread.Sleep(1000);

            // STOP MATCHES
            matchSimulator.StopMatch(1);
            Thread.Sleep(1000);
            matchSimulator.StopMatch(2);
            Thread.Sleep(1000);
        }
    }
}
