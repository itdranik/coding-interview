using FluentAssertions;
using ITDranik.CodingInterview.MathExpressions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ITDranik.CodingInterview.MathExpressionsTests {
    public class IntegrationTests {
        public IntegrationTests() {
            _tokenizer = new Tokenizer();
            _algorithm = new ShuntingYardAlgorithm();
            _calculator = new PostfixNotationCalculator();
        }

        [Fact]
        public void CalculateUsingAllSupportedOperators() {
            var actual = Calculate("(1+2)*3 - (2.2*1.1)/0.5 + 0.2");
            var expected = 4.36;

            actual.Should().BeApproximately(expected, Precision);
        }

        [Fact]
        public void RedundantOpeningBracket_SyntaxError() {
            Action action = () => Calculate("((2 + 3)");
            action.Should().ThrowExactly<SyntaxException>();
        }

        [Fact]
        public void RedundantClosingBracket_SyntaxError() {
            Action action = () => Calculate("(2+3))");
            action.Should().ThrowExactly<SyntaxException>();
        }

        [Fact]
        public void BracketSubExpression_ChangePrioritization() {
            var actual = Calculate(" (2 +2)* 2 ");
            var expected = 8;
            actual.Should().BeApproximately(expected, Precision);
        }

        [Fact]
        public void AdditionMultiplication_MultiplicationCalculatedFirst() {
            var actual = Calculate("2+2 * 2");
            var expected = 6;
            actual.Should().BeApproximately(expected, Precision);
        }

        private double Calculate(string expression) {
            var infixNotationTokens = _tokenizer.Parse(expression);
            var postfixNotationTokens =_algorithm.Apply(infixNotationTokens);
            return _calculator.Calculate(postfixNotationTokens).Value;
        }

        private const double Precision = 1e-7;

        private Tokenizer _tokenizer;
        private ShuntingYardAlgorithm _algorithm;
        private PostfixNotationCalculator _calculator;
    }
}
