using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace QueryStringGenerator.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Returns the class modifiers. Partial keyword is excluded due the fact that generated
        /// extension methods cannot be declared inside 'partial' classes. If no access modifier
        /// is specified returns 'internal'.
        /// </summary>
        public static string GetModifiers(this ClassDeclarationSyntax cds)
        {
            var modifiers = cds.Modifiers.Where(m => !m.IsKind(SyntaxKind.PartialKeyword));
            if (modifiers.Any())
            {
                return string.Join(" ", modifiers);
            }

            return "internal";
        }
    }
}
