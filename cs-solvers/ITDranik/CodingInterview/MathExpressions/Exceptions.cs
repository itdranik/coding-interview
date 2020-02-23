using System;

namespace ITDranik.CodingInterview.MathExpressions {

    public class MathExpressionException : Exception {
        public MathExpressionException(string message) : base(message) {
        }
    }

    public class SyntaxException : MathExpressionException {
        public SyntaxException(string message) : base(message) {
        }
    }

}
