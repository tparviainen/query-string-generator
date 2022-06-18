namespace QueryStringGenerator.App.Models;

[QueryString("ChildQueryString")]
internal class ChildClass : BaseClass
{
    public int? Year { get; set; }

    public string? Name { get; set; }
}
