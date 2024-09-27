namespace Logic.Tokens;

public class NumberToken(decimal number) : OperandToken(((double)number).ToString() == "-0" ? "0" : ((double)number).ToString())
{
    public decimal Number => decimal.Parse(Value);
}
