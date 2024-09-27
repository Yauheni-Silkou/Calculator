namespace Logic.Tokens;

public abstract class Token(string value)
{
    public string Value { get; set; } = value;

    public override string ToString() => Value;

    public static implicit operator Token(decimal value) => new NumberToken(value);

    public static implicit operator Token(double value) => new NumberToken((decimal)value);
}
