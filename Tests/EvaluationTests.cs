namespace Tests;

public class EvaluationTests(ITestOutputHelper output) : TestBase(output)
{
    [Theory]
    [InlineData("#00 - 000", "15.55555+8-3", "20.55555")]
    [InlineData("#00 - 001", "15.555+8-3", "20.555")]
    [InlineData("#00 - 002", "15.5+8-3", "20.5")]
    [InlineData("#00 - 003", "15+8-3", "20")]
    [InlineData("#00 - 004", "12-10+33", "35")]
    [InlineData("#00 - 005", "3*3-9", "0")]
    [InlineData("#00 - 006", "3*3-7", "2")]
    [InlineData("#00 - 007", "3*3-5", "4")]
    [InlineData("#00 - 008", "4*3-9", "3")]
    [InlineData("#00 - 009", "4*3-7", "5")]
    [InlineData("#00 - 010", "4*3-5", "7")]
    [InlineData("#00 - 011", "5-1*4+7", "8")]
    [InlineData("#00 - 012", "8*3-5*2", "14")]
    [InlineData("#00 - 013", "5.505*1000-5", "5500")]
    [InlineData("#00 - 014", "9*3+7-5+2*6-8/4+1", "40")]
    [InlineData("#00 - 015", "9*(3+7)-5+2*6-8/4+1", "96")]
    [InlineData("#00 - 016", "9*2^3", "72")]
    [InlineData("#00 - 017", "5^2 + 3   * 5^2", "100")]
    [InlineData("#00 - 018", "7^2-9+1-4^2", "25")]
    [InlineData("#00 - 019", "-5+5", "0")]
    [InlineData("#00 - 020", "4+(-5)*(-5)+(-2^2)", "25")]
    [InlineData("#00 - 021", "(3-2)+(3^2)", "10")]
    [InlineData("#00 - 022", "2^3^2", "512")]
    public void EvaluateExpression_ValidExpression_ReturnsAnswer(string number, string expression, string expected)
    {
        // Act
        var actual = ExpressionEvaluator.Evaluate(expression, out string formatted).Value;

        // Output
        bool equal = expected == actual;
        string status = equal ? "success" : "fail";
        string description = equal ? $"{formatted} = {actual}" : $"{formatted} = {actual} ({expected} expected)";
        _output.WriteLine($"Test {number} ({status}) : {description}");

        // Assert
        Assert.Equal(expected, actual);
    }
}
