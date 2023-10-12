using Gomoku.Application.Games.Commands.CreateGame;
using Gomoku.Application.Games.Commands.PlaceStone;
using Gomoku.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gomoku.Api.Controllers
{
    public class GameController : ApiControllerBase
    {
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Guid> CreateGame()
        {
            return await Mediator.Send(new CreateGameCommand());
        }

        [HttpPost("PlaceStone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> PlaceStone([FromBody] PlaceStoneCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
