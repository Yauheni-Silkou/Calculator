namespace Logic.Tokens;

public class TokenList : List<Token>
{
    public TokenList() : base() { }

    public TokenList(IEnumerable<Token> tokens) : base(tokens) { }

    public TokenList(string expression) : this(TokensManager.Tokenize(expression)) { }

    public override string ToString()
    {
        return Count > 0 ? JoinElements() : "No tokens in it";
    }

    private string JoinElements()
    {
        return string.Concat(this.Select(x => $"-[ {x.Value} ]-"))[1..^1];
    }
}
