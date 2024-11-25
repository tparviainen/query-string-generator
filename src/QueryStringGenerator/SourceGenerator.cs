using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using QueryStringGenerator.Services;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace QueryStringGenerator
{
    [Generator]
    internal class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(AddAttributeToCompilation);

            // Create a syntax provider that extracts the return type kind of candidate symbols
            IncrementalValuesProvider<ITypeSymbol?> types = context.SyntaxProvider.CreateSyntaxProvider(IsCandidateForGenerator, GetTypeSymbolForCandidate);

            // Filter out null types
            IncrementalValuesProvider<ITypeSymbol> finalTypes = types.Where(type => type is not null)!;

            // Collect the types into a single item
            IncrementalValueProvider<ImmutableArray<ITypeSymbol>> collected = finalTypes.Collect();

            context.RegisterSourceOutput(collected, GenerateCode);
        }

        private void GenerateCode(SourceProductionContext context, ImmutableArray<ITypeSymbol> array)
        {
            foreach (var type in array)
            {
                var builder = new SourceBuilder(type);

                var fileName = builder.GetFileName();
                var content = builder.GetSource();

                context.AddSource(fileName, content);
            }
        }

        private void AddAttributeToCompilation(IncrementalGeneratorPostInitializationContext context)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "QueryStringGenerator.QueryStringAttribute.cs";
            var sourceText = SourceText.From(assembly.GetManifestResourceStream(resourceName));

            context.AddSource("QueryStringAttribute.g.cs", sourceText.ToString());
        }

        /// <summary>
        /// Check whether the provided SyntaxNode is relevant for us or not. In Roslyn,
        /// a syntax tree represents the lexical and syntactic structure of source code.
        /// </summary>
        private bool IsCandidateForGenerator(SyntaxNode node, CancellationToken cancellationToken) =>
            node is ClassDeclarationSyntax { AttributeLists.Count: > 0 } or RecordDeclarationSyntax { AttributeLists.Count: > 0 };

        /// <summary>
        /// Access a semantic model and transform the node for downstream usage. Semantic
        /// model has all the information about the types defined in the namespace.
        /// </summary>
        private ITypeSymbol? GetTypeSymbolForCandidate(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var syntax = GetSyntaxForCandidate(context);
            if (syntax is null)
            {
                return null;
            }

            return context.SemanticModel.GetDeclaredSymbol(syntax, cancellationToken);
        }

        private TypeDeclarationSyntax? GetSyntaxForCandidate(GeneratorSyntaxContext context)
        {
            var typeDeclarationSyntax = (TypeDeclarationSyntax)context.Node;

            var attributeLists = typeDeclarationSyntax.ChildNodes().OfType<AttributeListSyntax>();

            foreach (var attributeList in attributeLists)
            {
                if (attributeList.Attributes.Any(IsQueryStringAttribute))
                {
                    return typeDeclarationSyntax;
                }
            }

            return null;
        }

        private static bool IsQueryStringAttribute(AttributeSyntax attribute) =>
            attribute.Name is SimpleNameSyntax { Identifier.ValueText: "QueryString" or "QueryStringAttribute" };
    }
}
