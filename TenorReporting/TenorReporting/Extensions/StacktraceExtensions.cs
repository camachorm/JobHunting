using System.Diagnostics;
using System.Linq;

namespace TenorReporting.Extensions
{
    public static class StackTraceExtensions
    {
        public static StackTrace TraceCodeLocation(this StackTrace source, int indentationLevel = 0)
        {
            Trace.TraceInformation(source.CaptureCodeLocation(indentationLevel));
            return source;
        }

        public static string CaptureCodeLocation(this StackTrace source, int indentationLevel = 0)
        {
            var frame = source?.GetFrames()?.First();

            return (frame != null
                ? $"FileName: {frame.GetFileName()}\tClass Name: {frame.GetMethod()?.DeclaringType?.FullName}\tMethod Name: {frame.GetMethod().Name}\tLine Number: {frame.GetFileLineNumber()}"
                : "Location unknown").Indent(indentLevel: indentationLevel);
        }
    }
}
