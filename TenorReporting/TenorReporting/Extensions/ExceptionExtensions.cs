using System;
using System.Diagnostics;

namespace TenorReporting.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Provides a fluent method to trace an exception consistently formatted and including all inner exceptions. Calls
        ///     Trace.TraceError()
        /// </summary>
        /// <param name="source">The exception to be added to Trace</param>
        /// <returns>The exception for fluent code syntax</returns>
        public static Exception TraceException(this Exception source)
        {
            Trace.TraceError(source.FormatException());
            return source;
        }

        /// <summary>
        ///     Provides a simple and clean recursive formatting of an Exception tree
        ///     (Exception::InnerException::InnerException::etc...)
        /// </summary>
        /// <param name="source">The source exception</param>
        /// <param name="indentation">The level of indentation to apply</param>
        /// <returns>The formatted message and stacktrace of the exception at the specified indentation level</returns>
        public static string FormatException(this Exception source, int indentation = 0)
        {
            if (source == null) return String.Empty;

            var indent = new string('\t', indentation);

            var result =
                $"{indent}Message: {source.Message}{Environment.NewLine}{indent}Type: {source.GetType().FullName}{Environment.NewLine}{indent}StackTrace: {source.StackTrace.Indent(indentLevel: indentation)}{Environment.NewLine}{indent}Code Location: {new StackTrace(source).CaptureCodeLocation()}{Environment.NewLine}";

            if (source.InnerException != null)
                result +=
                    $"{indent}Inner Exception:{Environment.NewLine}{source.InnerException.FormatException(++indentation)}{Environment.NewLine}";

            return result;
        }

        
        public static bool IsWrongfullyDisposed(this Exception source)
        {
            return source.Message.StartsWith("cannot access a disposed object",
                       StringComparison.InvariantCultureIgnoreCase) ||
                   (source.InnerException?.IsWrongfullyDisposed()).GetValueOrDefault();
        }
    }
}
