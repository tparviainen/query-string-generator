using System.Diagnostics;
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

// Assert the output
Debug.Assert(baseClass.ToQueryString() == "&id=42&description=C%23+Incremental+Generator+(from+base)&hierarchy=baseClass");
Debug.Assert(childClass.ToQueryString() == "&id=42&description=C%23+Incremental+Generator+(from+child)&hierarchy=childClass");
Debug.Assert(childClass.ChildQueryString() == "&year=24&name=CSSG");
Debug.Assert(model.ToQueryString() == "&limit=10&sort=Price");
