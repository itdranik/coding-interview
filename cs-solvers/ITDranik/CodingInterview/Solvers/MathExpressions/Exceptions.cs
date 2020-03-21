using System;

namespace ITDranik.CodingInterview.Solvers.MathExpressions {

    public class MathExpressionException : Exception {
        public MathExpressionException(string message) : base(message) {
        }
    }

    public class SyntaxException : MathExpressionException {
        public SyntaxException(string message) : base(message) {
        }
    }

}
