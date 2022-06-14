using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using QueryStringGenerator.Services;
using System;
using System.Reflection;

namespace QueryStringGenerator
{
    [Generator]
    internal class SourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not SyntaxReceiver receiver)
            {
                return;
            }

            try
            {
                foreach (var kvp in receiver.Contexts)
                {
                    var builder = new SourceBuilder(kvp);
                    builder.Build();

                    var fileName = builder.GetFileName();
                    var content = builder.GetSource();

                    context.AddSource(fileName, content);
                }
            }
            catch (Exception ex)
            {
                context.AddSource("Exception.g.cs", $"#if CSSG // {DateTime.Now}\n\n{ex}\n\n#endif\n");
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
            context.RegisterForPostInitialization(c =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "QueryStringGenerator.QueryStringAttribute.cs";
                var sourceText = SourceText.From(assembly.GetManifestResourceStream(resourceName));

                c.AddSource("QueryStringAttribute.g.cs", sourceText.ToString());
            });
        }
    }
}
