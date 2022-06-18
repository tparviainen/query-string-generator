namespace QueryStringGenerator.Tests.Models;

[QueryString("ToQueryStringFromStudent")]
public class Student : Person
{
    public School? School { get; set; }
}
