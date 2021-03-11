using Akka.Actor;
using AkkaActors.Matches.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AkkaActors.Matches
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ActorsEnvironment environment;

        public MatchesController(ActorsEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetMatchesResponse))]
        public async Task<IActionResult> Get()
        {
            var response = await environment
                .SelectActor(ActorPaths.MatchesActor)
                .Ask<GetMatchesResponse>(new GetMatchesRequest());

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Match match)
        {
            environment
                .SelectActor(ActorPaths.MatchesActor)
                .Tell(new MatchCreated { Match = match});

            return Ok();
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{matchId}/start")]
        public IActionResult StartMatch(int matchId)
        {
            environment
                .SelectActor($"{ActorPaths.MatchesActor}/{matchId}")
                .Tell(new MatchStarted { });

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{matchId}/stop")]
        public IActionResult StopMatch(int matchId)
        {
            environment
                .SelectActor($"{ActorPaths.MatchesActor}/{matchId}")
                .Tell(new MatchEnded { });

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{matchId}/changescore/{team1Score}/{team2Score}")]
        public IActionResult ChangeScore(int matchId, int team1Score, int team2Score)
        {
            environment
                .SelectActor($"{ActorPaths.MatchesActor}/{matchId}")
                .Tell(new MatchScoreChanged
                {
                    Team1Score = team1Score,
                    Team2Score = team2Score
                });

            return Ok();
        }
    }
}
