namespace Logic.Tokens;

public class UnaryOperatorToken(string @operator, OperandToken operand) : OperatorToken(@operator)
{
    private readonly OperandToken oper = operand;

    public override OperandToken Evaluate()
    {
        if (oper is null)
        {
            return new ErrorToken(CalculatorError.NullableValue);
        }
        else if (oper is ErrorToken error)
        {
            return error;
        }

        decimal n = (oper as NumberToken)!.Number;
        decimal result = Value switch
        {
            "-" => -n,
            _ => 0,
        };
        OperandToken token = this switch
        {
            { Value: "-" } => new NumberToken(result),
            _ => new ErrorToken(CalculatorError.InvalidOperation),
        };
        return token;
    }
}
