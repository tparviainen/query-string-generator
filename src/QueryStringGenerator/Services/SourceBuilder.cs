using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using QueryStringGenerator.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryStringGenerator.Services
{
    internal class SourceBuilder
    {
        private readonly SourceParams _params = new();
        private KeyValuePair<string, List<GeneratorSyntaxContext>> _kvp;

        public SourceBuilder(KeyValuePair<string, List<GeneratorSyntaxContext>> kvp)
        {
            _kvp = kvp;
        }

        internal void Build()
        {
            foreach (var syntaxContext in _kvp.Value)
            {
                var cds = (syntaxContext.Node as ClassDeclarationSyntax)!;
                var symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(cds)!;

                _params.Namespace = symbol.ContainingNamespace.ToString();
                _params.Modifiers = cds.GetModifiers();
                _params.ClassName = symbol.Name;
                _params.MethodName = symbol.GetMethodName();
                _params.Properties += AddProperties(syntaxContext, cds);
                _params.FileName = $"{cds.Identifier.ValueText}.g.cs";
            }
        }

        private string AddProperties(GeneratorSyntaxContext context, ClassDeclarationSyntax cds)
        {
            var properties = cds.DescendantNodes().OfType<PropertyDeclarationSyntax>();

            var sb = new StringBuilder();

            foreach (var property in properties)
            {
                var declaredSymbol = context.SemanticModel.GetDeclaredSymbol(property) as IPropertySymbol;

                if (declaredSymbol!.Type.IsValueType)
                {
                    if (declaredSymbol.NullableAnnotation != NullableAnnotation.Annotated)
                    {
                        // Currently only nullable value types are supported. If non-nullable types
                        // need to be supported, then need to clarify whether to include for example
                        // int=0 value to query string or not.
                        continue;
                    }

                    if (declaredSymbol.IsEnum())
                    {
                        sb.AppendLine(GetEnumType(declaredSymbol, property.Identifier.ValueText));
                    }
                    else
                    {
                        sb.AppendLine(GetValueType(property.Identifier.ValueText));
                    }
                }
                else
                {
                    sb.AppendLine(GetReferenceType(property.Identifier.ValueText));
                }
            }

            return sb.ToString();
        }

        private string GetEnumType(IPropertySymbol declaredSymbol, string text)
        {
            var type = ((INamedTypeSymbol)declaredSymbol.Type).TypeArguments.First();

            var sb = new StringBuilder();

            sb.AppendLine($@"
            if (_this.{text} != null)
            {{
                switch (_this.{text})
                {{");

            var members = type.GetMembers();
            foreach (var member in type.GetMembers().Where(m => m.Kind == SymbolKind.Field))
            {
                sb.AppendLine($@"                    case {member}:
                        sb.Append(""&{text.ToLower()}={char.ToLowerInvariant(member.Name[0])}{member.Name.Substring(1)}"");
                        break;
");
            }

            sb.Append($@"                    default:
                        throw new NotImplementedException();
                }}
            }}");

            return sb.ToString();
        }

        private string GetValueType(string text)
        {
            return $@"
            if (_this.{text} != null)
            {{
                sb.Append($""&{text.ToLower()}={{_this.{text}}}"");
            }}";
        }

        private string GetReferenceType(string text)
        {
            return $@"
            if (_this.{text} != null)
            {{
                sb.Append($""&{text.ToLower()}={{WebUtility.UrlEncode(_this.{text})}}"");
            }}";
        }

        internal string GetFileName() => _params.FileName;

        internal string GetSource()
        {
            return $@"// Auto-generated code
using System;
using System.Net;
using System.Text;

namespace {_params.Namespace}
{{
    {_params.Modifiers} static class QueryStringExtensionFor{_params.ClassName}
    {{
        public static string {_params.MethodName}(this {_params.ClassName} _this)
        {{
            if (_this == null)
            {{
                return string.Empty;
            }}

            var sb = new StringBuilder();
{_params.Properties}
            return sb.ToString();
        }}
    }}
}}
";
        }
    }
}
