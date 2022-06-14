namespace QueryStringGenerator.App.Models;

[QueryString("ChildQueryString")]
internal partial class ChildClass : BaseClass
{
    public int? Year { get; set; }

    public string? Name { get; set; }
}
