namespace QueryStringGenerator.Tests;

public class RecordTypeTests
{
    [Fact]
    public Task RecordTypeIsSupported()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public record Person(string FirstName, string LastName, int? Age);
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
