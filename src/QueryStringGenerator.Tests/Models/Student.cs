namespace QueryStringGenerator.Tests.Models;

[QueryString("ToQueryStringFromStudent")]
public partial class Student : Person
{
    public School? School { get; set; }
}
