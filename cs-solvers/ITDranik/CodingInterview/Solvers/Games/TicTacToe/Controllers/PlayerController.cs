using System.Collections.Generic;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models;

namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Controllers
{
    public class PlayerController
    {
        public PlayerController(Game game, Player player)
        {
            _game = game;
            _player = player;
        }

        public GameState State => _game.State;

        public IList<Cell> GetPossibleMoves()
        {
            return _game.GetPossibleMoves();
        }

        public void MakeMove(int row, int column)
        {
            _game.MakeMove(_player, new Cell(row, column));
        }

        private readonly Game _game;
        private readonly Player _player;
    }
}
