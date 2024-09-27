namespace Logic;

public static class ExpressionEvaluator
{
    public static Token Evaluate(string expression, out string formatted)
    {
        formatted = ExpressionFormatter.FormatExpression(expression);
        var tokens = new TokenList(formatted);
        var rpn = ShuntingYardAlgorithm.ConvertToRPN(tokens);
        var sya = ShuntingYardAlgorithm.EvaluateRPN(rpn);
        return sya;
    }
}
