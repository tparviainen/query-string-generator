# Query String Generator

C# source generator to create a method that returns the query string of the object.

# Usage

Install package

```
PM> Install-Package QueryStringGenerator
```

## Changes to Models

Class must be decorated with `QueryString` attribute, which is declared in `QueryStringGenerator` namespace.

```csharp
using QueryStringGenerator;

[QueryString]
public class Model
{
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string? Sort { get; set; }
}
```

## Get the Generated Query String

By default the generated method name is `ToQueryString`, which when called returns the query string of the object.

```csharp
var model = new Model
{
    Limit = 10,
    Sort = "Price"
};

Console.WriteLine($"Query string: {model.ToQueryString()}");

/*
This code example produces the following results:

Query string: &limit=10&sort=Price
*/
```

# Supported Data Types

- Nullable value types, including enums
- Reference types
