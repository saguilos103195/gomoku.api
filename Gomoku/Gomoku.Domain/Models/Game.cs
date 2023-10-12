using Gomoku.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Domain.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Board Board { get; set; }
        public Stone CurrentTurn { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            Board = new Board();
            CurrentTurn = Stone.Black;
        }
    }
}
