namespace QueryStringGenerator.Tests;

public class StudentTests
{
    [Fact]
    public Task ValueTypeEnumEncodedCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

public enum School
{
    JediAcademy,
    StarfleetAcademy,
    XaviersSchoolForGiftedYoungsters
};

[QueryString]
public class Student
{
    public School? School { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task BaseClassPropertiesHandledCorrectly()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

public enum School
{
    JediAcademy,
    StarfleetAcademy,
    XaviersSchoolForGiftedYoungsters
};

public class Person
{
    public int? Age { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

[QueryString(MethodName = "ToQueryStringFromStudent")]
public class Student : Person
{
    public School? School { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
