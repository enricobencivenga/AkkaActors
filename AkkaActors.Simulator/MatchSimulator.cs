using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace AkkaActors.Simulator
{
    public class MatchSimulator
    {
        private readonly HttpClient client;

        public MatchSimulator()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
        }

        public void CreateMatch(int id, string team1, string team2)
        {
            var result = client.PostAsync($"/api/matches", new StringContent(JsonConvert.SerializeObject(new
            {
                Id = id,
                Team1 = team1,
                Team2 = team2
            }), Encoding.UTF8, "application/json")).Result;
            result.EnsureSuccessStatusCode();
        }

        public void StartMatch(int matchId)
        {
            var result = client.PutAsync($"/api/matches/{matchId}/start", new StringContent("")).Result;
            result.EnsureSuccessStatusCode();
        }

        public void StopMatch(int matchId)
        {
            var result = client.PutAsync($"/api/matches/{matchId}/stop", new StringContent("")).Result;
            result.EnsureSuccessStatusCode();
        }

        public void ChangeScore(int matchId, int team1Score, int team2Score)
        {
            var result = client.PutAsync($"/api/matches/{matchId}/changescore/{team1Score}/{team2Score}", new StringContent("")).Result;
            result.EnsureSuccessStatusCode();
        }
    }
}
