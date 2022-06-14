using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace QueryStringGenerator
{
    internal class SyntaxReceiver : ISyntaxContextReceiver
    {
        /// <summary>
        /// Query string -attribute can be added to partial class and then there are multiple
        /// contexts that contain valid data.
        /// </summary>
        public Dictionary<string, List<GeneratorSyntaxContext>> Contexts { get; } = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            if (context.Node is ClassDeclarationSyntax classDeclarationSyntax)
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax)!;
                var attribute = symbol.GetAttributes().SingleOrDefault(a => a.AttributeClass?.Name == "QueryStringAttribute");

                if (attribute != default)
                {
                    var symbolName = symbol.ToString();

                    if (!Contexts.ContainsKey(symbolName))
                    {
                        Contexts.Add(symbolName, new List<GeneratorSyntaxContext>());
                    }

                    Contexts[symbolName].Add(context);
                }
            }
        }
    }
}
