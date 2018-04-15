#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

#endregion

namespace NAC.Framework
{
    [ExcludeFromCodeCoverage]
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
        {
        }

        public InvalidMoveException(string message) : base(message)
        {
        }

        public InvalidMoveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMoveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}