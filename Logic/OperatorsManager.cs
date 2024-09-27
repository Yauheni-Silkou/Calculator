namespace Logic;

public static class OperatorsManager
{
    private static readonly (string value, int arity, Associativity associativity)[] operators =
    [
        ("-", 1, Associativity.Left),
        ("!", 1, Associativity.Left),

        ("+", 2, Associativity.Left),
        ("−", 2, Associativity.Left),
        ("*", 2, Associativity.Left),
        ("/", 2, Associativity.Left),
        ("^", 2, Associativity.Right),

        ("(", 0, Associativity.Left),
        (")", 0, Associativity.Left),
    ];

    private static readonly Dictionary<string, int> arities = operators.Select(@operator => (@operator.value, @operator.arity)).ToDictionary();

    private static readonly Dictionary<string, Associativity> associativities = operators.Select(@operator => (@operator.value, @operator.associativity)).ToDictionary();

    private static readonly Dictionary<string, int> precedences = (new string[][]
    {
        ["(", ")"],
        ["+", "−"],
        ["*", "/"],
        ["-"],
        ["^"],
        ["!"],
    }).SelectMany((set, index) => set.Select(@operator => (@operator, index))).ToDictionary();

    public static int GetArity(this OperatorToken token)
    {
        return arities[token.Value];
    }

    public static Associativity GetAssociativity(this OperatorToken token)
    {
        return associativities[token.Value];
    }

    public static int GetOperatorPrecedence(this OperatorToken token)
    {
        return precedences[token.Value];
    }

    public static bool IsOperatorBinary(string @operator)
    {
        return operators.Where(o => o.arity == 2).Select(o => o.value).Contains(@operator);
    }
}
