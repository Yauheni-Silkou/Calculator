namespace Logic;

public static class ExpressionFormatter
{
    public static string FormatExpression(string expression)
    {
        var formatted = expression.Replace(" ", string.Empty)
            .Replace("\t", string.Empty)
            .Replace("-", "−")
            .Replace("(−", "(-");

        if (formatted.StartsWith('−'))
        {
            formatted = $"-{formatted[1..]}";
        }

        int openCount = formatted.Count(c => c == '(');
        int closeCount = formatted.Count(c => c == ')');
        if (openCount > closeCount)
        {
            formatted = $"{formatted}{new string(')', openCount - closeCount)}";
        }
        else if (openCount > closeCount)
        {
            formatted = $"{new string('(', closeCount - openCount)}{formatted}";
        }

        return formatted;
    }
}
