using FluentAssertions;
using ITDranik.CodingInterview.MathExpressions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ITDranik.CodingInterview.MathExpressionsTests.UnitTests {
    public class TokenizerTests {
        private Tokenizer _tokenizer;

        public TokenizerTests() {
            _tokenizer = new Tokenizer();
        }

        [Fact]
        public void Parse_EmptyExpression_EmptyCollection() {
            var actual = _tokenizer.Parse("       ");
            var expected = new List<IToken>() {};
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_PlusSign_AdditionOperator() {
            var actual = _tokenizer.Parse("+");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.Addition) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_MinusSign_SubtractionOperator() {
            var actual = _tokenizer.Parse("-");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.Subtraction) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_StarSign_MultiplicationOperator() {
            var actual = _tokenizer.Parse("*");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.Multiplication) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_SlashSign_DivisionOperator() {
            var actual = _tokenizer.Parse("/");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.Division) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_LeftRoundBracket_OpeningBracket() {
            var actual = _tokenizer.Parse("(");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.OpeningBracket) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_RightRoundBracket_ClosingBracket() {
            var actual = _tokenizer.Parse(")");
            var expected = new List<IToken>() { new OperatorToken(OperatorType.ClosingBracket) };
            Compare(actual, expected);
        }


        [Fact]
        public void Parse_NumberWithDecimalSeparator_DoubleNumber() {
            var actual = _tokenizer.Parse("2.25");
            var expected = new List<IToken>() { new OperandToken(2.25) };
            Compare(actual, expected);
        }

        [Fact]
        public void Parse_NumberWithTwoDecimalSeparators_SyntaxError() {
            Action action = () => _tokenizer.Parse("2.25.23");
            action.Should().ThrowExactly<SyntaxException>();
        }

        private void Compare(IEnumerable<IToken> actual, IEnumerable<IToken> expected) {
            actual.Should().BeEquivalentTo(expected,
                opts => opts.RespectingRuntimeTypes().WithStrictOrdering()
            );
        }
    }
}
