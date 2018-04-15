#region Usings

using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    public interface IRulesService
    {
        bool IsMovementAuthorized(Account originatorAccount, PaymentScheme scheme, decimal ammount);
    }
}