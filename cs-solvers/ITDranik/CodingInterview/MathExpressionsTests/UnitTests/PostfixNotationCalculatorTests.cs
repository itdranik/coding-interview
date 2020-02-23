using FluentAssertions;
using System.Collections.Generic;
using ITDranik.CodingInterview.MathExpressions;
using Xunit;
using System;

namespace ITDranik.CodingInterview.MathExpressionsTests.UnitTests {
    public class PostfixNotationCalculatorTests {

        private const double Precision = 1e-7;

        private PostfixNotationCalculator _calculator;

        public PostfixNotationCalculatorTests() {
            _calculator = new PostfixNotationCalculator();
        }

        [Fact]
        public void AdditionalOperatorTest() {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Addition)
            });

            Compare(actual, new OperandToken(3));
        }

        [Fact]
        public void SubtractionOperatorTest() {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Subtraction)
            });

            Compare(actual, new OperandToken(-1));
        }

        [Fact]
        public void MultiplicationOperatorTest() {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(4),
                new OperandToken(2),
                new OperatorToken(OperatorType.Multiplication)
            });

            Compare(actual, new OperandToken(8));
        }

        [Fact]
        public void DivisionOperatorTest() {
            var actual = _calculator.Calculate(new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Division)
            });

            Compare(actual, new OperandToken(0.5));
        }

        [Fact]
        public void StackingOperandsTest() {
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
        public void EmptyExpressionFailure() {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() {}));
        }

        [Fact]
        public void RedundantOperatorFailure() {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() {
                new OperatorToken(OperatorType.Addition)
            }));
        }

        [Fact]
        public void RedundantOperandFailure() {
            ExpectSyntaxException(() => _calculator.Calculate(new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2)
            }));
        }

        private void ExpectSyntaxException(Action action) {
            action.Should().ThrowExactly<SyntaxException>();
        }

        private void Compare(OperandToken actual, OperandToken expected) {
            actual.Value.Should().BeApproximately(expected.Value, Precision);
        }
    }
}
