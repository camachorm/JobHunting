#region Usings

using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}