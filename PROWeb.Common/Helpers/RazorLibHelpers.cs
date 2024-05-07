using System.Diagnostics;
using System.Reflection;


namespace PROWeb.Common.Helpers
{
    public class RazorLibHelpers
    {
        public static string GetWebRootPath()
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();

            if (method?.DeclaringType is { } type)
            {
                return $"_content/{Assembly.GetAssembly(type)?.GetName().Name}";
            }

            return $"_content/{Assembly.GetExecutingAssembly().GetName().Name}";
        }
    }
}
