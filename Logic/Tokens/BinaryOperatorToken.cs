namespace Logic.Tokens;

public class BinaryOperatorToken(string @operator, OperandToken leftOperand, OperandToken rightOperand) : OperatorToken(@operator)
{
    private readonly OperandToken left = leftOperand;
    private readonly OperandToken right = rightOperand;

    public override OperandToken Evaluate()
    {
        if (left is null || right is null)
        {
            return new ErrorToken(CalculatorError.NullableValue);
        }
        else if (left is ErrorToken lError)
        {
            return lError;
        }
        else if (right is ErrorToken rError)
        {
            return rError;
        }

        decimal l = (left as NumberToken)!.Number;
        decimal r = (right as NumberToken)!.Number;
        decimal result = Value switch
        {
            "+" => l + r,
            "−" => l - r,
            "*" => l * r,
            "/" when r == 0 => 0,
            "/" => l / r,
            "^" => (decimal)Math.Pow((double)l, (double)r),
            _ => 0,
        };
        OperandToken token = this switch
        {
            { Value: "/" } when r == 0 => new ErrorToken(CalculatorError.DivisionByZero),
            { Value: "+" or "−" or "*" or "/" or "^" } => new NumberToken(result),
            _ => new ErrorToken(CalculatorError.InvalidOperation),
        };
        return token;
    }
}
