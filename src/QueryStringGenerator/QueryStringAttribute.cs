// Auto-generated code
namespace QueryStringGenerator
{
    public class QueryStringAttribute : System.Attribute
    {
        public string MethodName { get; set; }

        public QueryStringAttribute()
        {
        }

        public QueryStringAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
