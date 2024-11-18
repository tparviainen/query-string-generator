using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace QueryStringGenerator.Tests;

public static class TestHelper
{
    public static Task Verify(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        // The GeneratorDriver is used to run generator against a compilation
        GeneratorDriver driver = GeneratorDriver(syntaxTree);

        // Use verify to snapshot test the incremental generator output!
        return Verifier.Verify(driver).UseDirectory("Verified");
    }

    public static GeneratorDriver GeneratorDriver(SyntaxTree syntaxTree)
    {
        IEnumerable<PortableExecutableReference> references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        ];

        // Create a Roslyn compilation for the syntax tree
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree],
            references: references,
            options: new CSharpCompilationOptions(
                outputKind: OutputKind.DynamicallyLinkedLibrary,
                nullableContextOptions: NullableContextOptions.Enable)
            );

        // Create an instance of incremental generator
        var generator = new SourceGenerator();

        // The GeneratorDriver is used to run generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the incremental generator! GeneratorDriver is an immutable class and
        // all calls return a mutated copy of the driver.
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var generatorDiagnostics);

        // Compilation diagnostics after executing generator
        var compilationDiagnostics = outputCompilation.GetDiagnostics();

        Debug.Assert(generatorDiagnostics.IsEmpty);
        Debug.Assert(compilationDiagnostics.IsEmpty);

        return driver;
    }
}
