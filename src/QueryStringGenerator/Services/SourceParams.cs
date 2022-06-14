namespace QueryStringGenerator.Services
{
    internal class SourceParams
    {
        public string? Namespace { get; set; } = "TemplateNamespace";
        public string? Modifiers { get; set; } = "internal partial";
        public string? ClassName { get; set; } = "TemplateClass";
        public string? MethodName { get; set; } = "TemplateMethod";
        public string? Properties { get; set; }
        public string FileName { get; set; } = "Template.g.cs";
    }
}
