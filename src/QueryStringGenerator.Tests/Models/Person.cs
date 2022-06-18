namespace QueryStringGenerator.Tests.Models;

[QueryString]
public class Person
{
    public string? FirstName { get; set; }
    public string LastName { get; set; } = default!;
    public int? Age { get; set; }
}
