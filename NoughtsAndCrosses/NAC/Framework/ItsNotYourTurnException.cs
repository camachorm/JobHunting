#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

#endregion

namespace NAC.Framework
{
    [ExcludeFromCodeCoverage]
    public class ItsNotYourTurnException : Exception
    {
        public ItsNotYourTurnException()
        {
        }

        public ItsNotYourTurnException(string message) : base(message)
        {
        }

        public ItsNotYourTurnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItsNotYourTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}