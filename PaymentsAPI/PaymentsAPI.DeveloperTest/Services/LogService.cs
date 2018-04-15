#region Usings

using System;
using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    /// <summary>
    ///     We provide an application wide log service so that we can customize log handling at our bespoke level.
    ///     For brevity I will skip expanding on it but I would point out that Logging Strategy is a complex topic that brings
    ///     up
    ///     discussions around many topics, including data sensitivity, data security, and support needs for future customer
    ///     care
    /// </summary>
    public class LogService : ILogService
    {
        public void LogException(Exception e)
        {
            // Code not included for brevity
        }

        /// <summary>
        ///     We use strong-types parameters intentionally for enforcing specific handling, as and when the generalization
        ///     surfaces,
        ///     we should add a generic method based on a common interface across Request/Result object types
        /// </summary>
        public void LogOperation(MakePaymentRequest request, MakePaymentResult result)
        {
            // Code not included for brevity
        }
    }
}