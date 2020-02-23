namespace ITDranik.CodingInterview.MathExpressions {

    public interface IToken {}

    public class OperandToken : IToken {
        public double Value { get { return _value; } }

        public OperandToken(double value) {
            _value = value;
        }

        private readonly double _value;
    }

    public enum OperatorType {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        OpeningBracket,
        ClosingBracket
    }

    public class OperatorToken : IToken {
        public OperatorType OperatorType { get { return _operatorType; } }

        public OperatorToken(OperatorType operatorType) {
            _operatorType = operatorType;
        }

        private readonly OperatorType _operatorType;
    }
}
