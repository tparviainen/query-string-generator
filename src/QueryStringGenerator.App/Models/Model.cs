namespace QueryStringGenerator.App.Models;

[QueryString(MethodName = "GetQueryString")]
public class Model
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string? Sort { get; set; }
}
