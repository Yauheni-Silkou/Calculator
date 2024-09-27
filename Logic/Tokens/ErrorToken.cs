namespace Logic.Tokens;

public class ErrorToken(CalculatorError error) : OperandToken(ConvertToMessage(error))
{
    public CalculatorError Error => Enum.GetValues(typeof(CalculatorError))
        .Cast<CalculatorError>()
        .FirstOrDefault(x => ConvertToMessage(x) == Value);

    private static string ConvertToMessage(CalculatorError error)
    {
        return InsertSpacesBeforeUppercase(error.ToString());
    }

    private static string InsertSpacesBeforeUppercase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var sb = new StringBuilder();
        sb.Append(char.ToUpper(input[0]));

        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                sb.Append(' ');
            }
            sb.Append(input[i]);
        }

        return sb.ToString().ToLower();
    }
}
