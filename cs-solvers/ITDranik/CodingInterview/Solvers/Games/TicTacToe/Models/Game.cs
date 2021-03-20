using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models
{
    public class Game
    {
        public GameState State => _state;

        public Game()
        {
            _state = GameState.FirstPlayerTurn;

            _board = new Board();
            _history = new Stack<HistoryItem>();

            _firstPlayer = new Player(PlayerMark.X);
            _secondPlayer = new Player(PlayerMark.O);
        }

        public Player FirstPlayer => _firstPlayer;
        public Player SecondPlayer => _secondPlayer;

        public IList<Cell> GetPossibleMoves()
        {
            if (_state == GameState.FirstPlayerVictory ||
                _state == GameState.SecondPlayerVictory ||
                _state == GameState.Draw
            ) {
                return new List<Cell>();
            }

            return _board.GetEmptyCells();
        }

        public void MakeMove(Player player, Cell cell)
        {
            if ((_state == GameState.FirstPlayerTurn && player.Mark != PlayerMark.X) ||
                (_state == GameState.SecondPlayerTurn && player.Mark != PlayerMark.O)
            ) {
                throw new InvalidMoveException("The turn belongs to another player.");
            }

            if (_board[cell.Row, cell.Column] != null)
            {
                throw new InvalidMoveException("The chosen cell is not empty.");
            }

            if (
                _state == GameState.FirstPlayerVictory ||
                _state == GameState.SecondPlayerVictory ||
                _state == GameState.Draw
            ) {
                throw new InvalidMoveException("The game was ended.");
            }

            _history.Push(new HistoryItem(_state, player.Mark, cell));

            _board[cell.Row, cell.Column] = player.Mark;
            _state = DeduceState(cell, player.Mark);
        }

        public void UndoMove()
        {
            if (_history.Count == 0)
            {
                throw new InvalidOperationException("The history is empty.");
            }

            var (previousState, _, cell) = _history.Pop();

            _state = previousState;

            Debug.Assert(_board[cell.Row, cell.Column] != null, "An invalid state or history.");
            _board[cell.Row, cell.Column] = null;
        }

        private GameState DeduceState(Cell cell, PlayerMark mark)
        {
            if (
                _board.IsRowFilledWithMark(cell.Row, mark) ||
                _board.IsColumnFilledWithMark(cell.Column, mark) ||
                _board.IsPrimaryDiagonalFilledWithMark(mark) ||
                _board.IsSecondaryDiagonalFilledWithMark(mark)
            ) {
                return mark switch
                {
                    PlayerMark.X => GameState.FirstPlayerVictory,
                    PlayerMark.O => GameState.SecondPlayerVictory,

                    _ => throw new InvalidMoveException("An unknown mark.")
                };
            }

            if (_board.GetEmptyCells().Count == 0)
            {
                return GameState.Draw;
            }

            return mark switch
            {
                PlayerMark.X => GameState.SecondPlayerTurn,
                PlayerMark.O => GameState.FirstPlayerTurn,

                _ => throw new InvalidMoveException("An unknown mark.")
            };
        }

        private GameState _state;
        private readonly Board _board;
        private readonly Stack<HistoryItem> _history;
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;
    }
}
