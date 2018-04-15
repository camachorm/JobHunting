#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

#endregion

namespace NAC.Framework
{
    [ExcludeFromCodeCoverage]
    public class GameOverException : Exception
    {
        public GameOverException()
        {
        }

        public GameOverException(string message) : base(message)
        {
        }

        public GameOverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameOverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}