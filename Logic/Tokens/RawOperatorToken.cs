namespace Logic.Tokens;

public class RawOperatorToken(string @operator) : OperatorToken(@operator)
{
    public OperatorToken Convert(OperandToken[] tokens)
    {
        return this.GetArity() switch
        {
            1 => new UnaryOperatorToken(Value, tokens[0]),
            2 => new BinaryOperatorToken(Value, tokens[0], tokens[1]),
            _ => this,
        };
    }

    public override OperandToken Evaluate()
    {
        return new ErrorToken(CalculatorError.NonevaluableToken);
    }
}
