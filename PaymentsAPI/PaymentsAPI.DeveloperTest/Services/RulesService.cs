#region Usings

using System;
using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    /// <summary>
    ///     We centralize our rules processing in an injectable service for further scalability of the codebase
    /// </summary>
    public class RulesService : IRulesService
    {
        public RulesService(ILogService logger)
        {
            Logger = logger;
        }

        private ILogService Logger { get; }

        public bool IsMovementAuthorized(Account originatorAccount, PaymentScheme scheme, decimal ammount)
        {
            // Avoid nesting for compiled code optimization
            if (originatorAccount == null ||
                !originatorAccount.AllowedPaymentSchemes.HasFlag((AllowedPaymentSchemes) scheme)) return false;

            var result = false;

            switch (scheme)
            {
                case PaymentScheme.FasterPayments:
                    result = originatorAccount.HasValidBalance(ammount);
                    break;
                case PaymentScheme.Bacs:
                    result = true;
                    break;
                case PaymentScheme.Chaps:
                    result = originatorAccount.Status == AccountStatus.Live;
                    break;
                default:
                    // Although we just return false (for security reasons), we still keep track of the exception for later analysis
                    // in a live system we might also include additional information required for future investigation taking sensitive data masking
                    // needs into account
                    // I also chose to maintain the default handler to deal with 
                    Logger.LogException(new ArgumentOutOfRangeException(nameof(scheme), scheme, null));
                    break;
            }

            return result;
        }
    }
}