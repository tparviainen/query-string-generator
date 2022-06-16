# Query String Generator

Generate a query string for a class with C# source generator.

# Usage

Install package

```
PM> Install-Package ...
```

## Changes to Models

Class must be declared `partial` and decorated with `QueryString` attribute.

```csharp
using QueryStringGenerator;

[QueryString]
public partial class Model
{
    // existing properties ...
}
```

## Get the Generated Query String

By default the generated method name is `ToQueryString`, which can be called to get the query string of the object.

```csharp
var model = new Model
{
    // set values to properties ...
};

Console.WriteLine($"Query string: {model.ToQueryString()}");
```

# Supported Data Types

- Nullable value types, including enums
- Reference types
