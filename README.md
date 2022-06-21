# Query String Generator

C# source generator to create a method that returns the query string of the object.

# Usage

## 1. Install the [NuGet](https://www.nuget.org/packages/QueryStringGenerator) package

```
PM> Install-Package QueryStringGenerator
```

## 2. Update the Model(s)

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

## 3. Call `ToQueryString` Method to the Instance of the Class

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

# Generated Source Code

Below is the auto-generated extension method for the class defined in step 2. above.

```csharp
// Auto-generated code
using System;
using System.Net;
using System.Text;

namespace QueryStringGenerator.App.Models
{
    public static class QueryStringExtensionForModel
    {
        public static string ToQueryString(this Model _this)
        {
            if (_this == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            if (_this.Limit != null)
            {
                sb.Append($"&limit={_this.Limit}");
            }

            if (_this.Offset != null)
            {
                sb.Append($"&offset={_this.Offset}");
            }

            if (_this.Sort != null)
            {
                sb.Append($"&sort={WebUtility.UrlEncode(_this.Sort)}");
            }

            return sb.ToString();
        }
    }
}
```

# Supported Data Types

- Nullable value types, including enums
- Reference types

**NOTE:** The query string _value_ for enum is the name of the enum starting with a lowercase character.
