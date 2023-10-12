using Gomoku.Application.Interfaces;
using Gomoku.Domain.Enums;
using Gomoku.Domain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public Game CreateGame()
        {
            var game = new Game();
            games[game.Id] = game;
            return game;
        }

        public Game GetGame(Guid id) => games.GetValueOrDefault(id);

        public bool ContainsGame(Guid id) => games.ContainsKey(id);

        public void ResetGame(Guid id)
        {
            var game = games[id];
            game.CurrentTurn = Stone.Black;
            ResetBoard(game.Board);
        }

        public void ResetBoard(Board board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    board.Grid[i, j] = Stone.None;
                }
            }
        }
    }
}
