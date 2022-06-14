using Microsoft.CodeAnalysis;
using System.Linq;

namespace QueryStringGenerator.Extensions
{
    internal static class SymbolExtensions
    {
        /// <summary>
        /// Gets the query string method name either given via attribute or default name.
        /// </summary>
        public static string GetMethodName(this ISymbol symbol)
        {
            var attribute = symbol.GetAttributes().Single(a => a.AttributeClass?.Name == "QueryStringAttribute");
            var methodName = attribute.ConstructorArguments.FirstOrDefault().Value as string;

            if (string.IsNullOrWhiteSpace(methodName))
            {
                methodName = "ToQueryString";
            }

            return methodName!;
        }

        public static bool IsEnum(this IPropertySymbol symbol) =>
            ((INamedTypeSymbol)symbol.Type).TypeArguments.FirstOrDefault()?.TypeKind == TypeKind.Enum;
    }
}
