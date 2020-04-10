using FluentAssertions;
using System.Collections.Generic;
using ITDranik.CodingInterview.Solvers.MathExpressions;
using Xunit;
using FluentAssertions.Equivalency;
using System.Linq;
using System;

namespace ITDranik.CodingInterview.SolversTests.MathExpressions.UnitTests
{
    public class ShuntingYardAlgorithmTests
    {
        public ShuntingYardAlgorithmTests()
        {
            _algorithm = new ShuntingYardAlgorithm();
        }

        [Theory]
        [InlineData(OperatorType.Addition)]
        [InlineData(OperatorType.Subtraction)]
        [InlineData(OperatorType.Multiplication)]
        [InlineData(OperatorType.Division)]
        public void BinaryOperatorTest(OperatorType operatorType)
        {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(operatorType),
                new OperandToken(2)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(operatorType)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void BinaryOperatorsWithBracketsTest()
        {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperatorToken(OperatorType.OpeningBracket),
                new OperandToken(1),
                new OperatorToken(OperatorType.Addition),
                new OperandToken(2),
                new OperatorToken(OperatorType.ClosingBracket),
                new OperatorToken(OperatorType.Multiplication),
                new OperandToken(3)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Addition),
                new OperandToken(3),
                new OperatorToken(OperatorType.Multiplication)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void BinaryOperatorsWithDifferentPrioritiesTest()
        {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Addition),
                new OperandToken(2),
                new OperatorToken(OperatorType.Multiplication),
                new OperandToken(3)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperandToken(3),
                new OperatorToken(OperatorType.Multiplication),
                new OperatorToken(OperatorType.Addition)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void SequentialDivisionOperatorsTest()
        {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Division),
                new OperandToken(2),
                new OperatorToken(OperatorType.Division),
                new OperandToken(3)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Division),
                new OperandToken(3),
                new OperatorToken(OperatorType.Division)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void RedundantOpeningBracketTest()
        {
            Action action = () => _algorithm.Apply(new List<IToken>() {
                new OperatorToken(OperatorType.OpeningBracket)
            });
            action.Should().ThrowExactly<SyntaxException>();
        }

        [Fact]
        public void RedundantClosingBracketTest()
        {
            Action action = () => _algorithm.Apply(new List<IToken>() {
                new OperatorToken(OperatorType.ClosingBracket)
            });
            action.Should().ThrowExactly<SyntaxException>();
        }

        private void Compare(IEnumerable<IToken> actual, IEnumerable<IToken> expected)
        {
            actual.Should().BeEquivalentTo(expected,
                opts => opts.RespectingRuntimeTypes().WithStrictOrdering()
            );
        }

        private readonly ShuntingYardAlgorithm _algorithm;
    }
}
