using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITDranik.CodingInterview.MathExpressions {

    public class Tokenizer {
        public Tokenizer() {
            _valueTokenBuilder = new StringBuilder();
            _infixNotationTokens = new List<IToken>();
        }

        public IEnumerable<IToken> Parse(string expression) {
            Reset();
            foreach (char next in expression) {
                FeedCharacter(next);
            }
            return GetResult();
        }

        private void Reset() {
            _valueTokenBuilder.Clear();
            _infixNotationTokens.Clear();
        }

        private void FeedCharacter(char next) {
            if (IsSpacingCharacter(next)) {
                if (_valueTokenBuilder.Length > 0) {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }
            } else if (IsOperatorCharacter(next)) {
                if (_valueTokenBuilder.Length > 0) {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }

                var operatorToken = CreateOperatorToken(next);
                _infixNotationTokens.Add(operatorToken);
            } else {
                _valueTokenBuilder.Append(next);
            }
        }

        private bool IsOperatorCharacter(char c) {
            switch (c) {
                case '(':
                case ')':
                case '+':
                case '-':
                case '*':
                case '/':
                    return true;
                default:
                    return false;
            }
        }

        private bool IsSpacingCharacter(char c) {
            switch (c) {
                case ' ':
                    return true;
                default:
                    return false;
            }
        }

        private IToken CreateOperandToken(string raw) {
            if (double.TryParse(raw, out double result)) {
                return new OperandToken(result);
            }

            throw new SyntaxException($"The operand {raw} has an invalid format.");
        }

        private OperatorToken CreateOperatorToken(char c) {
            switch (c) {
                case '(': return new OperatorToken(OperatorType.OpeningBracket);
                case ')': return new OperatorToken(OperatorType.ClosingBracket);
                case '+': return new OperatorToken(OperatorType.Addition);
                case '-': return new OperatorToken(OperatorType.Subtraction);
                case '*': return new OperatorToken(OperatorType.Multiplication);
                case '/': return new OperatorToken(OperatorType.Division);
                default:
                    throw new SyntaxException($"There's no a suitable operator for the char {c}");
            }
        }

        private IEnumerable<IToken> GetResult() {
            if (_valueTokenBuilder.Length > 0) {
                var token = CreateOperandToken(_valueTokenBuilder.ToString());
                _valueTokenBuilder.Clear();
                _infixNotationTokens.Add(token);
            }

            return _infixNotationTokens.ToList();
        }

        private readonly StringBuilder _valueTokenBuilder;
        private readonly List<IToken> _infixNotationTokens;
    }

}
