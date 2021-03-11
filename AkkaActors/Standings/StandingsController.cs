using Akka.Actor;
using AkkaActors.Standings.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AkkaActors.Standings
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly ActorsEnvironment environment;

        public StandingsController(ActorsEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetStandingsResponse))]
        public async Task<IActionResult> Get()
        {
            var response = await environment
                .SelectActor(ActorPaths.StandingsActor)
                .Ask<GetStandingsResponse>(new GetStandingsRequest());

            return Ok(response);
        }
    }
}
