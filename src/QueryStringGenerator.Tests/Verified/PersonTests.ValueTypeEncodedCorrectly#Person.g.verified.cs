﻿//HintName: Person.g.cs
// <auto-generated />

namespace QueryStringGenerator.Tests.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("QueryStringGenerator", "1.0.0")]
    public static class QueryStringExtensionForPerson
    {
        public static string ToQueryString(this Person _this)
        {
            if (_this == null)
            {
                return string.Empty;
            }

            var sb = new System.Text.StringBuilder();

            if (_this.Age != null)
            {
                sb.Append($"&age={_this.Age}");
            }

            return sb.ToString();
        }
    }
}
