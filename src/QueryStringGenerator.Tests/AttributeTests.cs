namespace QueryStringGenerator.Tests;

public class AttributeTests
{
    [Fact]
    public Task ClassWithoutQueryStringAttributeDoesNotGenerateExtensionMethod()
    {
        // The source code to test
        var source = """
using System;

namespace QueryStringGenerator.Tests.Models;

[Serializable]
public class Person
{
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task QueryStringWithoutAttributeSuffixIsFoundFromClass()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task QueryStringWithAttributeSuffixIsFoundFromClass()
    {
        // The source code to test
        var source = """
namespace QueryStringGenerator.Tests.Models;

[QueryStringAttribute]
public class Person
{
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }

    [Fact]
    public Task QueryStringAttributeWithOtherAttributesIsFoundFromClass()
    {
        // The source code to test
        var source = """
using System;

namespace QueryStringGenerator.Tests.Models;

[QueryString]
[Serializable]
public class Person
{
    public int? Age { get; set; }
}
""";

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}
