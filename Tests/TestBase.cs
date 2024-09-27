using Xunit.Abstractions;

namespace Tests;

public abstract class TestBase(ITestOutputHelper output)
{
    protected readonly ITestOutputHelper _output = output;
}
