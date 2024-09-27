namespace Logic.Tokens;

public static class TokensManager
{
    public static List<Token> Tokenize(string expression)
    {
        var chars = expression.Select(c => (value: c.ToString(), isDigit: char.IsDigit(c) || c == '.'));

        var tokens = string.Concat(chars.Select(c => c.isDigit ? c.value : $" {c.value} "))
            .Split(" ")
            .Where(str => !string.IsNullOrWhiteSpace(str))
            .Select(token => decimal.TryParse(token, out decimal number) ? new NumberToken(number) : (Token)new RawOperatorToken(token));

        return tokens.ToList();
    }
}
