using FluentAssertions;
using ITDranik.CodingInterview.Solvers.MathExpressions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.MathExpressions.UnitTests {
    public class TokenizerTests {
        private Tokenizer _tokenizer;

        public TokenizerTests() {
            _tokenizer = new Tokenizer();
        }

        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public void EmptyExpressionTest(string expression) {
            var actual = _tokenizer.Parse(expression);
            var expected = new List<IToken>() {};
            Compare(actual, expected);
        }

        [Theory]
        [InlineData("+", OperatorType.Addition)]
        [InlineData("-", OperatorType.Subtraction)]
        [InlineData("*", OperatorType.Multiplication)]
        [InlineData("/", OperatorType.Division)]
        [InlineData("(", OperatorType.OpeningBracket)]
        [InlineData(")", OperatorType.ClosingBracket)]
        public void OperatorTest(string expression, OperatorType expectedType) {
            var actual = _tokenizer.Parse(expression);
            var expected = new List<IToken>() { new OperatorToken(expectedType) };
            Compare(actual, expected);
        }

        [Theory]
        [InlineData("2.25", 2.25)]
        [InlineData("0.1", 0.1)]
        public void CorrectOperandTest(string expression, double expectedValue) {
            var actual = _tokenizer.Parse(expression);
            var expected = new List<IToken>() { new OperandToken(expectedValue) };
            Compare(actual, expected);
        }

        [Theory]
        [InlineData("2.25.23")]
        public void IncorrectOperandTest(string expression) {
            Action action = () => _tokenizer.Parse(expression);
            action.Should().ThrowExactly<SyntaxException>();
        }

        private void Compare(IEnumerable<IToken> actual, IEnumerable<IToken> expected) {
            actual.Should().BeEquivalentTo(expected,
                opts => opts.RespectingRuntimeTypes().WithStrictOrdering()
            );
        }
    }
}
