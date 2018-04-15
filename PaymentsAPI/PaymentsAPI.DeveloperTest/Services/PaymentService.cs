#region Usings

using System.Data;
using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        /// <summary>
        ///     By applying dependency injection we make the purpose of the service more clear and segment the additional
        ///     responsitibilities
        ///     into injectable components
        /// </summary>
        public PaymentService(IDataStoreService dataStoreService, ILogService logService, IRulesService rulesService)
        {
            DataStoreService = dataStoreService;
            LogService = logService;
            RulesService = rulesService;
        }

        private IDataStoreService DataStoreService { get; }
        private ILogService LogService { get; }
        private IRulesService RulesService { get; }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            // Although the implementation of the datastores is quite straightforward, we will more likely than not, want to expand it 
            // by making it runtime configurable, in a more dynamic fashion than an app.config file
            var dataStore = DataStoreService.GetAccountDataStore();

            var account = dataStore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult
            {
                Success = RulesService.IsMovementAuthorized(account, request.PaymentScheme, request.Amount)
            };

            if (!result.Success)
            {
                LogService.LogException(
                    new ConstraintException("Unable to make payment, rules engine rejected the operation"));
                return result;
            }

            account.Balance -= request.Amount;

            // Update debtor account
            dataStore.UpdateAccount(account);
            // Question pending: does the creditor account not need to have the funds deposited?

            // Log the successfull operation
            LogService.LogOperation(request, result);

            return result;
        }
    }
}