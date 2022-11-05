using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace QueryStringGenerator.Extensions
{
    internal static class SymbolExtensions
    {
        private static readonly string _methodName = "MethodName";
        private static readonly string _queryStringAttribute = "QueryStringAttribute";

        /// <summary>
        /// Gets the query string method name. The name is either named argument or constructor argument value.
        /// <exception cref="ArgumentNullException">Thrown when MethodName is not valid.</exception>
        /// </summary>
        public static string GetMethodName(this ISymbol symbol)
        {
            var attribute = symbol.GetAttributes().Single(a => a.AttributeClass?.Name == _queryStringAttribute);

            var namedArgument = attribute.NamedArguments.FirstOrDefault(na => na.Key == _methodName).Value.Value as string;
            if (!string.IsNullOrWhiteSpace(namedArgument))
            {
                return namedArgument!;
            }

            var constructorArgument = attribute.ConstructorArguments.FirstOrDefault().Value as string;
            if (!string.IsNullOrWhiteSpace(constructorArgument))
            {
                return constructorArgument!;
            }

            throw new ArgumentNullException($"{_queryStringAttribute} has invalid {_methodName}");
        }

        public static bool IsEnum(this IPropertySymbol symbol) =>
            ((INamedTypeSymbol)symbol.Type).TypeArguments.FirstOrDefault()?.TypeKind == TypeKind.Enum;
    }
}
