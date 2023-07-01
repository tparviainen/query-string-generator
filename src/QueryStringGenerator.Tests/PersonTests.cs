namespace QueryStringGenerator.Tests;

[UsesVerify]
public class PersonTests
{
    [Fact]
    public Task ValueTypeEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    public int? Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task ValueTypeWithConstructorArgumentsEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString("ToQueryStringFromPerson")]
public class Person
{
    public int? Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task ValueTypesEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    public int? Age { get; set; }
    public int? Weight { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task ReferenceTypeEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    public string? FirstName { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task ReferenceTypeWithNamedArgumentsEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString(MethodName = "ToQueryStringFromPerson")]
public class Person
{
    public string? FirstName { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
