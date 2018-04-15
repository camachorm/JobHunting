#region Usings

using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

#endregion

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class RulesServiceTests
    {
        // For brevity I am not mocking the logservice here as it is just a stub
        private readonly IRulesService _service = new RulesService(new LogService());

        [Test]
        public void NullAccountFails()
        {
            // Test if FasterPayments payment fails (unauthorized payment scheme)
            var result = _service.IsMovementAuthorized(null, PaymentScheme.FasterPayments, 5000);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestBacsFails()
        {
            // Test if Bacs payment fails (unauthorized PaymentScheme)
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 5000
            }, PaymentScheme.Bacs, 5000);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestBacsSucceeds()
        {
            // Test if Bacs payment succeeds
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000
            }, PaymentScheme.Bacs, 5000);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestChapsFails()
        {
            // Test if Chaps payment fails (unauthorized payment scheme)
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000,
                Status = AccountStatus.Live
            }, PaymentScheme.Chaps, 5000);

            Assert.IsFalse(result);

            // Test if Chaps payment fails (wrong account status)
            result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000,
                Status = AccountStatus.Disabled
            }, PaymentScheme.Chaps, 5000);

            Assert.IsFalse(result);

            // Test if Chaps payment fails (wrong account status)
            result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000,
                Status = AccountStatus.InboundPaymentsOnly
            }, PaymentScheme.Chaps, 5000);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestChapsSucceeds()
        {
            // Test if Chaps payment succeeds
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Balance = 5000,
                Status = AccountStatus.Live
            }, PaymentScheme.Chaps, 5000);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestFasterPaymentsFails()
        {
            // Test if FasterPayments payment fails (unauthorized payment scheme)
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000
            }, PaymentScheme.FasterPayments, 5000);

            Assert.IsFalse(result);

            // Test if FasterPayments payment fails (insuficient balance)
            result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 5000
            }, PaymentScheme.FasterPayments, 5001);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestFasterPaymentsSucceeds()
        {
            // Test if FasterPayments payment succeeds
            var result = _service.IsMovementAuthorized(new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 5000
            }, PaymentScheme.FasterPayments, 5000);

            Assert.IsTrue(result);
        }
    }
}