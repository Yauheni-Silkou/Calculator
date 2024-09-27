namespace Logic;

public class ShuntingYardAlgorithm
{
    public static TokenList ConvertToRPN(TokenList tokens)
    {
        var output = new TokenList();
        var holdingStack = new Stack<OperatorToken>();

        foreach (var token in tokens)
        {
            if (token is OperandToken operandToken)
            {
                output.Add(operandToken);
            }
            else if (token is OperatorToken operatorToken)
            {
                if (operatorToken.IsLeftParenthesis)
                {
                    holdingStack.Push(operatorToken);
                }
                else if (operatorToken.IsRightParenthesis)
                {
                    OperatorToken popped;
                    do
                    {
                        if (!holdingStack.Any(t => t.IsLeftParenthesis))
                        {
                            return [new ErrorToken(CalculatorError.MismatchedParentheses)];
                        }

                        popped = holdingStack.Pop();
                        if (popped.IsLeftParenthesis)
                        {
                            break;
                        }

                        output.Add(popped);
                    }
                    while (true);
                }
                else
                {
                    int otp = operatorToken.Precedence;
                    int stp = holdingStack.Count > 0 ? holdingStack.Peek().Precedence : -1;

                    bool comparisonByPrecedence = operatorToken.Associativity switch
                    {
                        Associativity.Left => otp <= stp,
                        Associativity.Right => otp < stp,
                        _ => throw new ArgumentException(nameof(operatorToken.Associativity)),
                    };

                    while (holdingStack.Count > 0 && comparisonByPrecedence)
                    {
                        output.Add(holdingStack.Pop());
                    }

                    holdingStack.Push(operatorToken);
                }
            }
        }

        while (holdingStack.Count > 0)
        {
            output.Add(holdingStack.Pop());
        }

        return output;
    }

    public static Token EvaluateRPN(TokenList rpn)
    {
        var solveStack = new Stack<OperandToken>();

        foreach (var token in rpn)
        {
            if (token is ErrorToken)
            {
                return token;
            }
            else if (token is OperandToken operandToken)
            {
                solveStack.Push(operandToken);
            }
            else if (token is RawOperatorToken operatorToken)
            {
                OperandToken evaluated;
                switch (operatorToken.GetArity())
                {
                    case 1:
                        if (solveStack.Count <= 0)
                        {
                            return new ErrorToken(CalculatorError.InvalidOperation);
                        }

                        OperandToken operand = solveStack.Pop();

                        evaluated = operatorToken.Convert([operand]).Evaluate();
                        if (evaluated is ErrorToken)
                        {
                            return evaluated;
                        }

                        solveStack.Push(evaluated);
                        break;
                    case 2:
                        if (solveStack.Count <= 0)
                        {
                            return new ErrorToken(CalculatorError.InvalidOperation);
                        }

                        OperandToken right = solveStack.Pop();

                        if (solveStack.Count <= 0)
                        {
                            return new ErrorToken(CalculatorError.InvalidOperation);
                        }

                        OperandToken left = solveStack.Pop();

                        evaluated = operatorToken.Convert([left, right]).Evaluate();
                        if (evaluated is ErrorToken)
                        {
                            return evaluated;
                        }

                        solveStack.Push(evaluated);
                        break;
                }
            }
        }

        return solveStack.Pop();
    }
}
