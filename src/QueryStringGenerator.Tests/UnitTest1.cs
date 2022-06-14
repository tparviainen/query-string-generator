using System.Net;

namespace QueryStringGenerator.Tests;

[QueryString]
public partial class Person
{
    public string? FirstName { get; set; }
    public string LastName { get; set; } = default!;
    public int? Age { get; set; }
}

public class Student : Person
{
    public string? Institute { get; set; }
}

public class UnitTest1
{
    [Theory]
    [InlineData("John", "Doe")]
    public void ReferenceTypeValuesEncodedCorrectly(string firstName, string lastName)
    {
        // Arrange
        var person = new Person
        {
            FirstName = firstName,
            LastName = lastName
        };

        // Act
        var queryString = person.ToQueryString();

        // Assert
        Assert.Equal($"&firstname={WebUtility.UrlEncode(firstName)}&lastname={WebUtility.UrlEncode(lastName)}", queryString);
    }
}
