using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ITDranik.CodingInterview.Solvers.MathExpressions
{

    public class Tokenizer
    {
        public Tokenizer()
        {
            _valueTokenBuilder = new StringBuilder();
            _infixNotationTokens = new List<IToken>();
        }

        public IEnumerable<IToken> Parse(string expression)
        {
            Reset();
            foreach (char next in expression)
            {
                FeedCharacter(next);
            }
            return GetResult();
        }

        private void Reset()
        {
            _valueTokenBuilder.Clear();
            _infixNotationTokens.Clear();
        }

        private void FeedCharacter(char next)
        {
            if (IsSpacingCharacter(next))
            {
                if (_valueTokenBuilder.Length > 0)
                {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }
            }
            else if (IsOperatorCharacter(next))
            {
                if (_valueTokenBuilder.Length > 0)
                {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }

                var operatorToken = CreateOperatorToken(next);
                _infixNotationTokens.Add(operatorToken);
            }
            else
            {
                _valueTokenBuilder.Append(next);
            }
        }

        private static bool IsOperatorCharacter(char c) => c switch
        {
            var x when new char[]{'(', ')', '+', '-', '*', '/'}.Contains(x) => true,
            _ => false
        };

        private static bool IsSpacingCharacter(char c)
        {
            return c switch
            {
                ' ' => true,
                _ => false,
            };
        }

        private static IToken CreateOperandToken(string raw)
        {
            if (double.TryParse(
                raw,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out double result))
            {
                return new OperandToken(result);
            }

            throw new SyntaxException($"The operand {raw} has an invalid format.");
        }

        private static OperatorToken CreateOperatorToken(char c)
        {
            return c switch
            {
                '(' => new OperatorToken(OperatorType.OpeningBracket),
                ')' => new OperatorToken(OperatorType.ClosingBracket),
                '+' => new OperatorToken(OperatorType.Addition),
                '-' => new OperatorToken(OperatorType.Subtraction),
                '*' => new OperatorToken(OperatorType.Multiplication),
                '/' => new OperatorToken(OperatorType.Division),
                _ => throw new SyntaxException($"There's no a suitable operator for the char {c}"),
            };
        }

        private IEnumerable<IToken> GetResult()
        {
            if (_valueTokenBuilder.Length > 0)
            {
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
