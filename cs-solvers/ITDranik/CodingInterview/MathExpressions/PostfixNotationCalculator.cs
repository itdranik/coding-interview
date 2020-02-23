using System;
using System.Collections.Generic;

namespace ITDranik.CodingInterview.MathExpressions {
    public class PostfixNotationCalculator {
        public PostfixNotationCalculator() {
            _operandTokensStack = new Stack<OperandToken>();
        }

        public OperandToken Calculate(IEnumerable<IToken> tokens) {
            Reset();
            foreach (var token in tokens) {
                ProcessToken(token);
            }
            return GetResult();
        }

        private void Reset() {
            _operandTokensStack.Clear();
        }

        private void ProcessToken(IToken token) {
            switch (token) {
                case OperandToken operandToken:
                    StoreOperand(operandToken);
                    break;
                case OperatorToken operatorToken:
                    ApplyOperator(operatorToken);
                    break;
                default:
                    var exMessage = $"An unknown token type: {token.GetType().ToString()}.";
                    throw new SyntaxException(exMessage);
            }
        }

        private void StoreOperand(OperandToken operandToken) {
            _operandTokensStack.Push(operandToken);
        }

        private void ApplyOperator(OperatorToken operatorToken) {
            switch (operatorToken.OperatorType) {
                case OperatorType.Addition:
                    ApplyAdditionOperator();
                    break;
                case OperatorType.Subtraction:
                    ApplySubtractionOperator();
                    break;
                case OperatorType.Multiplication:
                    ApplyMultiplicationOperator();
                    break;
                case OperatorType.Division:
                    ApplyDivisionOperator();
                    break;
                default:
                    var exMessage = $"An unknown operator type: {operatorToken.OperatorType}.";
                    throw new SyntaxException(exMessage);
            }
        }

        private void ApplyAdditionOperator() {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value + operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplySubtractionOperator() {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value - operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplyMultiplicationOperator() {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value * operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplyDivisionOperator() {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value / operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private Tuple<OperandToken, OperandToken> GetBinaryOperatorArguments() {
            if (_operandTokensStack.Count < 2) {
                var exMessage = "Not enough arguments for applying a binary operator.";
                throw new SyntaxException(exMessage);
            }

            var right = _operandTokensStack.Pop();
            var left = _operandTokensStack.Pop();

            return Tuple.Create(left, right);
        }

        private OperandToken GetResult() {
            if (_operandTokensStack.Count == 0) {
                var exMessage = "The expression is invalid." +
                    " Check, please, that the expression is not empty.";
                throw new SyntaxException(exMessage);
            }

            if (_operandTokensStack.Count != 1) {
                var exMessage = "The expression is invalid." +
                    " Check, please, that you're providing the full expression and" +
                    " the tokens have a correct order.";
                throw new SyntaxException(exMessage);
            }

            return _operandTokensStack.Pop();
        }

        private readonly Stack<OperandToken> _operandTokensStack;
    }
}
