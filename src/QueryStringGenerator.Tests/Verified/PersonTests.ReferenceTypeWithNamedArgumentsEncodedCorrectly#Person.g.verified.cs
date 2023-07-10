﻿//HintName: Person.g.cs
// <auto-generated />

namespace QueryStringGenerator.Tests.Models
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("QueryStringGenerator", "1.0.0")]
    public static class QueryStringExtensionForPerson
    {
        public static string ToQueryStringFromPerson(this Person _this)
        {
            if (_this == null)
            {
                return string.Empty;
            }

            var sb = new global::System.Text.StringBuilder();

            if (_this.FirstName != null)
            {
                sb.Append($"&firstname={System.Net.WebUtility.UrlEncode(_this.FirstName)}");
            }

            return sb.ToString();
        }
    }
}
