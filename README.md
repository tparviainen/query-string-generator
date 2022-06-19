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
    // existing properties ...
}
```

## Get the Generated Query String

By default the generated method name is `ToQueryString`, which when called returns the query string of the object.

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
