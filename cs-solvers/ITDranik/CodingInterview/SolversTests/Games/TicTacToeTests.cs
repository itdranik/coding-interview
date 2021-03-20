using System;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Controllers;
using ITDranik.CodingInterview.Solvers.Games.TicTacToe.Models;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Games
{
    public class TicTacToeTests
    {
        public TicTacToeTests()
        {
            var game = new Game();
            _firstPlayer = new PlayerController(game, game.FirstPlayer);
            _secondPlayer = new PlayerController(game, game.SecondPlayer);
        }

        [Fact]
        public void WhenNewGame_ThenTurnBelongsToFirstPlayer()
        {
            _firstPlayer.State.Should().Be(GameState.FirstPlayerTurn);
        }

        [Fact]
        public void GivenFirstPlayerTurn_WhenFirstPlayerMoves_ThenTurnGoesToSecondPlayer()
        {
            _firstPlayer.MakeMove(0, 0);

            _firstPlayer.State.Should().Be(GameState.SecondPlayerTurn);
        }

        [Fact]
        public void GivenSecondPlayerTurn_WhenSecondPlayerMoves_ThenTurnGoesToFirstPlayer()
        {
            _firstPlayer.MakeMove(0, 0);
            _secondPlayer.MakeMove(0, 1);

            _firstPlayer.State.Should().Be(GameState.FirstPlayerTurn);
        }

        [Fact]
        public void GivenFirstPlayerTurn_WhenFirstPlayerMakesThreeXs_ThenFirstPlayerWins()
        {
            _firstPlayer.MakeMove(0, 0);
            _secondPlayer.MakeMove(0, 1);
            _firstPlayer.MakeMove(1, 0);
            _secondPlayer.MakeMove(0, 2);

            _firstPlayer.MakeMove(2, 0);

            _firstPlayer.State.Should().Be(GameState.FirstPlayerVictory);
        }

        [Fact]
        public void GivenSecondPlayerTurn_WhenSecondPlayerMakesThreeOs_SecondPlayerWins()
        {
            _firstPlayer.MakeMove(0, 0);
            _secondPlayer.MakeMove(0, 1);
            _firstPlayer.MakeMove(1, 0);
            _secondPlayer.MakeMove(1, 1);
            _firstPlayer.MakeMove(2, 2);

            _secondPlayer.MakeMove(2, 1);

            _firstPlayer.State.Should().Be(GameState.SecondPlayerVictory);
        }

        [Fact]
        public void GivenFirstPlayerWon_WhenSecondPlayerMakesMove_ThrowInvalidMoveException()
        {
            _firstPlayer.MakeMove(0, 0);
            _secondPlayer.MakeMove(0, 1);
            _firstPlayer.MakeMove(1, 0);
            _secondPlayer.MakeMove(0, 2);
            _firstPlayer.MakeMove(2, 0);

            Action action = () => _firstPlayer.MakeMove(2, 2);
            action.Should().ThrowExactly<InvalidMoveException>();
        }

        [Fact]
        public void GivenOneEmptyCell_WhenFirstPlayerMovesAndDoesNotMakeThreeXs_ThenDraw()
        {
            _firstPlayer.MakeMove(1, 1);
            _secondPlayer.MakeMove(0, 0);
            _firstPlayer.MakeMove(0, 1);
            _secondPlayer.MakeMove(2, 1);
            _firstPlayer.MakeMove(0, 2);
            _secondPlayer.MakeMove(2, 0);
            _firstPlayer.MakeMove(1, 0);
            _secondPlayer.MakeMove(1, 2);

            _firstPlayer.MakeMove(2, 2);

            _firstPlayer.State.Should().Be(GameState.Draw);
        }

        [Fact]
        public void WhenPlayerMovesToOccupiedCell_ThrowInvalidMoveException()
        {
            _firstPlayer.MakeMove(0, 0);

            Action action = () => _secondPlayer.MakeMove(0, 0);
            action.Should().ThrowExactly<InvalidMoveException>();
        }

        private readonly PlayerController _firstPlayer;
        private readonly PlayerController _secondPlayer;
    }
}
