using Gomoku.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Domain.Models
{
    public class Board
    {
        public int Size = 15;
        public Stone[,] Grid;
        public Board()
        {
            Grid = new Stone[Size, Size];
        }
    }
}
