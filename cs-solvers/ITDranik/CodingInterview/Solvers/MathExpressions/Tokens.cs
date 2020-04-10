namespace ITDranik.CodingInterview.Solvers.MathExpressions
{

    public interface IToken { }

    public class OperandToken : IToken
    {
        public double Value { get; }

        public OperandToken(double value)
        {
            Value = value;
        }
    }

    public enum OperatorType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        OpeningBracket,
        ClosingBracket
    }

    public class OperatorToken : IToken
    {
        public OperatorType OperatorType { get; }

        public OperatorToken(OperatorType operatorType)
        {
            OperatorType = operatorType;
        }
    }
}
