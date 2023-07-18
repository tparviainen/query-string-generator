namespace QueryStringGenerator.Tests;

[UsesVerify]
public class NonNullableTests
{
    [Fact]
    public Task NonNullableValueTypeIsNotPartOfTheGeneratedOutput()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    public int Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
