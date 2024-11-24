namespace QueryStringGenerator.Tests;

public class AccessibilityTests
{
    [Fact]
    public Task ProtectedPropertyIsNotPartOfTheGeneratedOutput()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    protected int? PersonId { get; set; }
    public int? Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task PrivatePropertyIsNotPartOfTheGeneratedOutput()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    private int? PersonId { get; set; }
    public int? Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
