using Gomoku.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Application.Interfaces
{
    public interface IGameService
    {
        Game CreateGame();
        Game GetGame(Guid id);
        bool ContainsGame(Guid id);
        void ResetGame(Guid id);
    }
}
