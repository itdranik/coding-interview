using FluentAssertions;
using System.Collections.Generic;
using ITDranik.CodingInterview.MathExpressions;
using Xunit;
using FluentAssertions.Equivalency;
using System.Linq;
using System;

namespace ITDranik.CodingInterview.MathExpressionsTests.UnitTests {
    public class ShuntingYardAlgorithmTests {
        private ShuntingYardAlgorithm _algorithm;

        public ShuntingYardAlgorithmTests() {
            _algorithm = new ShuntingYardAlgorithm();
        }

        [Fact]
        public void Apply_AdditionOperation_TwoNumbersAndAdditionOperator() {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Addition),
                new OperandToken(2)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Addition)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void Apply_SubtractionOperation_TwoNumbersAndSubtractionOperator() {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Subtraction),
                new OperandToken(2)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Subtraction)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void Apply_MultiplicationOperation_TwoNumbersAndMultiplicationOperator() {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Multiplication),
                new OperandToken(2)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Multiplication)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void Apply_DivisionOperation_TwoNumbersAndDivisionOperator() {
            var actual = _algorithm.Apply(new List<IToken>() {
                new OperandToken(1),
                new OperatorToken(OperatorType.Division),
                new OperandToken(2)
            });

            var expected = new List<IToken>() {
                new OperandToken(1),
                new OperandToken(2),
                new OperatorToken(OperatorType.Division)
            };

            Compare(actual, expected);
        }

        [Fact]
        public void Apply_AdditionInBracketsAndMultiplication_AdditionThenMultiplication() {
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
        public void Apply_AdditionAndMultiplication_MultiplicationThenAddition() {
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
        public void Apply_SequentialDivisions_SameOrder() {
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
        public void Apply_RedundantOpeningBracket_SyntaxError() {
            Action action = () => _algorithm.Apply(new List<IToken>() {
                new OperatorToken(OperatorType.OpeningBracket)
            });
            action.Should().ThrowExactly<SyntaxException>();
        }

        [Fact]
        public void Apply_RedundantClosingBracket_SyntaxError() {
            Action action = () => _algorithm.Apply(new List<IToken>() {
                new OperatorToken(OperatorType.ClosingBracket)
            });
            action.Should().ThrowExactly<SyntaxException>();
        }

        private void Compare(IEnumerable<IToken> actual, IEnumerable<IToken> expected) {
            actual.Should().BeEquivalentTo(expected,
                opts => opts.RespectingRuntimeTypes().WithStrictOrdering()
            );
        }
    }
}
