namespace Logic.Tokens;

public abstract class OperatorToken(string @operator) : Token(@operator)
{
    public Associativity Associativity => this.GetAssociativity();

    public int Precedence => this.GetOperatorPrecedence();

    public bool IsLeftParenthesis => Value == "(";

    public bool IsRightParenthesis => Value == ")";

    public abstract OperandToken Evaluate();
}
