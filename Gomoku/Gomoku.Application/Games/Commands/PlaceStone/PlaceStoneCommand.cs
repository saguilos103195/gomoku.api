using Gomoku.Application.Interfaces;
using Gomoku.Domain.Enums;
using Gomoku.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Gomoku.Application.Games.Commands.PlaceStone
{
    public class PlaceStoneCommand : IRequest<string>
    {
        public Guid GameId { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }

    public class PlaceStoneCommandHandler : IRequestHandler<PlaceStoneCommand, string>
    {
        private readonly IGameService _gameService;
        private int _gameSize = 0;

        public PlaceStoneCommandHandler(IGameService gameService)
        {
            _gameService = gameService;
        }

        public Task<string> Handle(PlaceStoneCommand request, CancellationToken cancellationToken)
        {
            if (!_gameService.ContainsGame(request.GameId))
            {
                throw new Exception("Game not found!");
            }

            var game = _gameService.GetGame(request.GameId);
            _gameSize = game.Board.Size;

            var win = PlaceStone(game, request.XCoordinate, request.YCoordinate);

            return Task.FromResult(win);
        }

        public string PlaceStone(Game game, int x, int y)
        {
            if (!IsValidPosition(x, y))
            {
                throw new Exception("Invalid Position!");
            }

            if (game.Board.Grid[x, y] != Stone.None)
                throw new Exception("Position already occupied!");

            game.Board.Grid[x, y] = game.CurrentTurn;

            var checkWin = CheckWin(x, y, game);
            Console.WriteLine(ConvertGridToString(game));

            game.CurrentTurn = game.CurrentTurn == Stone.Black ? Stone.White : Stone.Black;

            return checkWin;
        }

        private string ConvertGridToString(Game game)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _gameSize; i++)
            {
                for (int j = 0; j < _gameSize; j++)
                {
                    switch (game.Board.Grid[i, j])
                    {
                        case Stone.Black:
                            sb.Append('B');
                            break;
                        case Stone.White:
                            sb.Append('W');
                            break;
                        default:
                            sb.Append('.');
                            break;
                    }
                }
                sb.AppendLine();
            }
            
            return sb.ToString();
        }
        private string CheckWin(int x, int y, Game game)
        {
            var stone = game.CurrentTurn;
            char stoneChar = stone == Stone.Black ? 'B' : 'W';
            string winPattern = new string(stoneChar, 5);

            string horizontalLine = string.Concat(Enumerable.Range(0, _gameSize).Select(col => game.Board.Grid[x, col] == Stone.Black ? 'B' : (game.Board.Grid[x, col] == Stone.White ? 'W' : '.')));
            if (horizontalLine.Contains(winPattern))
            {
                _gameService.ResetGame(game.Id);
                return stoneChar == 'B' ? "Black Wins" : "White Wins";
            }
                

            string verticalLine = string.Concat(Enumerable.Range(0, _gameSize).Select(row => game.Board.Grid[row, y] == Stone.Black ? 'B' : (game.Board.Grid[row, y] == Stone.White ? 'W' : '.')));
            if (verticalLine.Contains(winPattern))
            {
                _gameService.ResetGame(game.Id);
                return stoneChar == 'B' ? "Black Wins" : "White Wins";
            }

            StringBuilder diagonalTLBR = new StringBuilder();
            for (int dx = 0, dy = 0; IsValidPosition(x + dx, y + dy); dx++, dy++)
            {
                diagonalTLBR.Append(game.Board.Grid[x + dx, y + dy] == Stone.Black ? 'B' : (game.Board.Grid[x + dx, y + dy] == Stone.White ? 'W' : '.'));
            }
            for (int dx = -1, dy = -1; IsValidPosition(x + dx, y + dy); dx--, dy--)
            {
                diagonalTLBR.Insert(0, game.Board.Grid[x + dx, y + dy] == Stone.Black ? 'B' : (game.Board.Grid[x + dx, y + dy] == Stone.White ? 'W' : '.'));
            }
            if (diagonalTLBR.ToString().Contains(winPattern))
            {
                _gameService.ResetGame(game.Id);
                return stoneChar == 'B' ? "Black Wins" : "White Wins";
            }

            StringBuilder diagonalBLTR = new StringBuilder();
            for (int dx = 0, dy = 0; IsValidPosition(x + dx, y - dy); dx++, dy++)
            {
                diagonalBLTR.Append(game.Board.Grid[x + dx, y - dy] == Stone.Black ? 'B' : (game.Board.Grid[x + dx, y - dy] == Stone.White ? 'W' : '.'));
            }
            for (int dx = -1, dy = -1; IsValidPosition(x + dx, y - dy); dx--, dy--)
            {
                diagonalBLTR.Insert(0, game.Board.Grid[x + dx, y - dy] == Stone.Black ? 'B' : (game.Board.Grid[x + dx, y - dy] == Stone.White ? 'W' : '.'));
            }
            if (diagonalBLTR.ToString().Contains(winPattern))
            {
                _gameService.ResetGame(game.Id);
                return stoneChar == 'B' ? "Black Wins" : "White Wins";
            }

            return stoneChar == 'B' ? "White Turn" : "Black Turn";
        }

        bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _gameSize && y >= 0 && y < _gameSize;
        }

    }


}
