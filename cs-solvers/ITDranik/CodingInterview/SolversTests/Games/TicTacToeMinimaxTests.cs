using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Controllers;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Games
{
    public class TicTacToeMinimaxTests
    {
        public TicTacToeMinimaxTests()
        {
            var game = new Game();

            _firstPlayer = new PlayerController(game, game.FirstPlayer);
            _secondPlayer = new PlayerController(game, game.SecondPlayer);

            _aiPlayer = new MinimaxPlayerController(
                game,
                new MinimaxScoreFunction()
            );
        }

        [Fact]
        public void FirstAiPlayerWinsInOneMove()
        {
            _firstPlayer.MakeMove(0, 0);
            _secondPlayer.MakeMove(0, 1);
            _firstPlayer.MakeMove(1, 0);
            _secondPlayer.MakeMove(1, 1);

            _aiPlayer.MakeMove();

            _firstPlayer.State.Should().Be(GameState.FirstPlayerVictory);
        }

        [Fact]
        public void FirstAiPlayerWinsInThreeMoves()
        {
            _firstPlayer.MakeMove(1, 1);
            _secondPlayer.MakeMove(2, 1);

            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();

            _firstPlayer.State.Should().Be(GameState.FirstPlayerVictory);
        }

        [Fact]
        public void DrawGame()
        {
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();
            _aiPlayer.MakeMove();

            _firstPlayer.State.Should().Be(GameState.Draw);
        }

        private readonly PlayerController _firstPlayer;
        private readonly PlayerController _secondPlayer;
        private readonly MinimaxPlayerController _aiPlayer;
    }
}
