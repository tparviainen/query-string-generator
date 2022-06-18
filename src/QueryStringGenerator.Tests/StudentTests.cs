using QueryStringGenerator.Tests.Models;

namespace QueryStringGenerator.Tests;

public class StudentTests
{
    [Fact]
    public void ValueTypeEnumEncodedCorrectly()
    {
        // Arrange
        var student = new Student
        {
            School = School.JediAcademy
        };

        // Act
        var queryString = student.ToQueryStringFromStudent();

        // Assert
        Assert.Equal("&school=jediAcademy", queryString);
    }

    [Fact]
    public void BaseClassPropertiesHandledCorrectly()
    {
        // Arrange
        var student = new Student
        {
            FirstName = "Luke",
            LastName = "Skywalker",
            School = School.JediAcademy
        };

        // Act
        var queryString = student.ToQueryStringFromStudent() + student.ToQueryString();

        // Assert
        Assert.Equal("&school=jediAcademy&firstname=Luke&lastname=Skywalker", queryString);
    }

    [Fact]
    public void UnknownEnumValueShouldThrowException()
    {
        // Arrange
        var student = new Student
        {
            School = (School)42
        };

        // Act
        var action = () => student.ToQueryStringFromStudent();

        // Assert
        Assert.Throws<NotImplementedException>(action);
    }
}
