using System;
using System.Collections.Generic;
using System.Diagnostics;
using ITDranik.CodingInterview.Solvers.Games.AI.Minimax;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models;

namespace ITDranik.CodingInterview.Solvers.Games.TicTacToe.Controllers
{
    public class MinimaxPlayerController
    {
        public MinimaxPlayerController(
            Game game,
            IScoreFunction<MinimaxAdapter, Player> scoreFunction
        ) {
            _game = game;
            _minimax = new Minimax<MinimaxAdapter, Player, Cell>(scoreFunction);
        }

        public void MakeMove()
        {
            var minimaxAdapter = new MinimaxAdapter(_game);
            var cell = _minimax.FindBestMove(minimaxAdapter, null);

            Debug.Assert(minimaxAdapter.ActivePlayer != null);
            _game.MakeMove(minimaxAdapter.ActivePlayer, cell);
        }

        private readonly Game _game;
        private readonly Minimax<MinimaxAdapter, Player, Cell> _minimax;
    }

    public class MinimaxAdapter : IState<Player, Cell>
    {
        public MinimaxAdapter(Game game)
        {
            _game = game;
        }

        public Game Game => _game;

        public bool IsTerminal => _game.State switch
        {
            var x when
                x == GameState.FirstPlayerVictory ||
                x == GameState.SecondPlayerVictory ||
                x == GameState.Draw => true,

            _ => false
        };

        public Player? ActivePlayer => _game.State switch
        {
            GameState.FirstPlayerTurn => _game.FirstPlayer,
            GameState.SecondPlayerTurn => _game.SecondPlayer,

            _ => null
        };

        public IList<Cell> GetPossibleMoves()
        {
            return _game.GetPossibleMoves();
        }

        public void MakeMove(Cell cell)
        {
            if (ActivePlayer == null)
            {
                var exMessage = "There is no an active player. Probably the game has been ended.";
                throw new InvalidOperationException(exMessage);
            }
            _game.MakeMove(ActivePlayer, cell);
        }

        public void UndoMove()
        {
            _game.UndoMove();
        }

        private readonly Game _game;
    }

    public class MinimaxScoreFunction : IScoreFunction<MinimaxAdapter, Player>
    {
        public double Calculate(MinimaxAdapter minimaxAdapter, Player player)
        {
            if (player == minimaxAdapter.Game.FirstPlayer)
            {
                return CalculateForFirstPlayer(minimaxAdapter);
            }
            else
            {
                return CalculateForSecondPlayer(minimaxAdapter);
            }
        }

        private static double CalculateForFirstPlayer(MinimaxAdapter state)
        {
            return state.Game.State switch
            {
                GameState.FirstPlayerVictory => 10.0,
                GameState.SecondPlayerVictory => -10.0,
                _ => 0.0
            };
        }

        private static double CalculateForSecondPlayer(MinimaxAdapter state)
        {
            return state.Game.State switch
            {
                GameState.FirstPlayerVictory => -10.0,
                GameState.SecondPlayerVictory => 10.0,
                _ => 0.0
            };
        }
    }
}
