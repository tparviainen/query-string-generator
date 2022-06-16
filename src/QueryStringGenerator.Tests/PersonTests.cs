using QueryStringGenerator.Tests.Models;
using System.Net;

namespace QueryStringGenerator.Tests;

public class PersonTests
{
    [Fact]
    public void ValueTypeEncodedCorrectly()
    {
        // Arrange
        var person = new Person
        {
            Age = 42
        };

        // Act
        var queryString = person.ToQueryString();

        // Assert
        Assert.Equal("&age=42", queryString);
    }

    [Theory]
    [InlineData("Kylo", "Ren")]
    [InlineData("Jean-Luc", "Picard")]
    [InlineData("Raven", "Darkhölme")]
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
