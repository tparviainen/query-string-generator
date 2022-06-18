namespace QueryStringGenerator.App.Models;

[QueryString]
internal class BaseClass
{
    public enum Inheritance
    {
        BaseClass,
        ChildClass
    }

    public int? Id { get; set; }

    public string? Description { get; set; }

    public Inheritance? Hierarchy { get; set; }
}
