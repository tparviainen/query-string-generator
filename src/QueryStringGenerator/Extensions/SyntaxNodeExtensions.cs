using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace QueryStringGenerator.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Returns the class modifiers. Partial keyword is excluded due the fact that extension
        /// methods cannot be declared inside 'partial static class'.
        /// </summary>
        public static string GetModifiers(this ClassDeclarationSyntax cds) =>
            string.Join(" ", cds.Modifiers.Where(m => !m.IsKind(SyntaxKind.PartialKeyword)));
    }
}
