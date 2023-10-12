using Gomoku.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<Guid>
    {
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IGameService _gameService;

        public CreateGameCommandHandler(IGameService gameService)
        {
            _gameService = gameService;
        }

        public Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = _gameService.CreateGame();
            return Task.FromResult(game.Id);
        }
    }
}
