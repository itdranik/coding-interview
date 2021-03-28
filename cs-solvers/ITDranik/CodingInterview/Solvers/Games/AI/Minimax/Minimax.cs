using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ITDranik.CodingInterview.Solvers.Games.AI.Minimax
{
    public class Minimax<State, Player, Move>
        where State: IState<Player, Move>
        where Player: class
        where Move: class
    {
        public Minimax(IScoreFunction<State, Player> scoreFunction)
        {
            _scoreFunction = scoreFunction;
        }

        public Move FindBestMove(State state, int? maxDepth)
        {
            if (state.IsTerminal)
            {
                var exMessage = "There should be no moves for a terminal state.";
                throw new InvalidOperationException(exMessage);
            }

            var activePlayer = state.ActivePlayer;
            if (activePlayer == null)
            {
                var exMessage = "There must be an active player for an intermediate state.";
                throw new InvalidOperationException(exMessage);
            }

            var (_, nextMove) = Estimate(state, activePlayer, maxDepth);

            Debug.Assert(nextMove != null, "A move must exist for an intermediate state.");
            return nextMove;
        }

        private (double Score, Move? NextMove) Estimate(State state, Player player, int? depth)
        {
            if (state.IsTerminal)
            {
                return
                (
                    Score: _scoreFunction.Calculate(state, player),
                    NextMove: null
                );
            }

            if (depth.HasValue && depth <= 0)
            {
                return
                (
                    Score: _scoreFunction.Calculate(state, player),
                    NextMove: null
                );
            }

            var possibleMoves = state.GetPossibleMoves();
            if (possibleMoves.Count == 0)
            {
                var exMessage = "At least one move must exist for an intermediate state.";
                throw new InvalidOperationException(exMessage);
            }

            var estimations = possibleMoves.Select((move) => {
                state.MakeMove(move);
                var (score, _) = Estimate(state, player, depth.HasValue ? depth - 1 : null);
                state.UndoMove();

                return (Score: score, Move: move);
            });

            var isOpponentTurn = player != state.ActivePlayer;

            return isOpponentTurn
                ? estimations.Aggregate((e1, e2) => e1.Score < e2.Score ? e1 : e2)
                : estimations.Aggregate((e1, e2) => e1.Score > e2.Score ? e1 : e2);
        }

        private readonly IScoreFunction<State, Player> _scoreFunction;
    }

    public interface IState<Player, Move>
        where Player: class
        where Move: class
    {
        IList<Move> GetPossibleMoves();
        void MakeMove(Move move);
        void UndoMove();

        bool IsTerminal { get; }
        Player? ActivePlayer { get; }
    }

    public interface IScoreFunction<State, Player>
    {
        double Calculate(State state, Player player);
    }
}
