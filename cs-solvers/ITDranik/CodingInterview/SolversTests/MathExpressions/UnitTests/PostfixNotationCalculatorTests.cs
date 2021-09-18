using FluentAssertions;
using System.Collections.Generic;
using ITDranik.CodingInterview.Solvers.MathExpressions;
using Xunit;
using System;

namespace ITDranik.CodingInterview.SolversTests.MathExpressions.UnitTests
{
    public class PostfixNotationCalculatorTests
    {

        private const double Precision = 1e-7;

        public PostfixNotationCalculatorTests()
        {
            _calculator = new PostfixNotationCalculator();
        }

        [Theory]
        [InlineData(1, 2, OperatorType.Addition, 3)]
        [InlineData(1, 2, OperatorType.Subtraction, -1)]
        [InlineData(4, 2, OperatorType.Multiplication, 8)]
        [InlineData(1, 2, OperatorType.Division, 0.5)]
        public void BinaryOperatorTest(
            double lhs,
            double rhs,
            OperatorType operatorType,
            double expectedValue
        )
        {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(lhs),
                new OperandToken(rhs),
                new OperatorToken(operatorType)
            });

            Compare(actual, new OperandToken(expectedValue));
        }

        [Fact]
        public void StackingOperandsTest()
        {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(4),
                new OperandToken(2),
                new OperandToken(3),
                new OperatorToken(OperatorType.Subtraction),
                new OperatorToken(OperatorType.Multiplication),
                new OperandToken(1),
                new OperatorToken(OperatorType.Addition)
            });

            Compare(actual, new OperandToken(-3));
        }

        [Fact]
        public void EmptyExpressionFailureTest()
        {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() { }));
        }

        [Fact]
        public void RedundantOperatorFailureTest()
        {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() {
                new OperatorToken(OperatorType.Addition)
            }));
        }

        [Fact]
        public void RedundantOperandFailureTest()
        {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2)
            }));
        }

        private static void ExpectSyntaxException(Action action)
        {
            action.Should().ThrowExactly<SyntaxException>();
        }

        private static void Compare(OperandToken actual, OperandToken expected)
        {
            actual.Value.Should().BeApproximately(expected.Value, Precision);
        }

        private readonly PostfixNotationCalculator _calculator;
    }
}
