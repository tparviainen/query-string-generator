using QueryStringGenerator.App.Models;
using static QueryStringGenerator.App.Models.BaseClass;

var baseClass = new BaseClass
{
    Hierarchy = Inheritance.BaseClass,
    Description = "C# Incremental Generator (from base)",
    Id = 42
};

var childClass = new ChildClass
{
    Hierarchy = Inheritance.ChildClass,
    Description = "C# Incremental Generator (from child)",
    Year = 24,
    Id = 42,
    Name = "CSSG"
};

Console.WriteLine("Generated query strings:");
Console.WriteLine("- base: " + baseClass.ToQueryString());
Console.WriteLine("- child: " + childClass.ToQueryString() + childClass.ChildQueryString());

/*
 * README.md example model and the output
 */
var model = new Model
{
    Limit = 10,
    Sort = "Price"
};

Console.WriteLine($"Query string: {model.ToQueryString()}");
