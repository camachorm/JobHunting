#region Usings

using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

#endregion

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private IDataStoreService DataStore { get; }
        private ILogService Logger { get; }

        // We could expand slightly by injecting mocked rules service instances as well, but for brevity I will skip it
        private IPaymentService PaymentService => new PaymentService(DataStore, Logger, new RulesService(Logger));


        public PaymentServiceTests()
        {
            // Setup our mocks
            var dataStoreMock = new Mock<IAccountDataStore>();
            dataStoreMock.Setup(i => i.GetAccount("1")).Returns(new Account
            {
                AccountNumber = "1",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps |
                                        AllowedPaymentSchemes.FasterPayments,
                Balance = 5000,
                Status = AccountStatus.InboundPaymentsOnly
            });
            dataStoreMock.Setup(i => i.GetAccount("2")).Returns(new Account
            {
                AccountNumber = "2",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps | AllowedPaymentSchemes.FasterPayments,
                Balance = 5000,
                Status = AccountStatus.Disabled
            });
            dataStoreMock.Setup(i => i.GetAccount("3")).Returns(new Account
            {
                AccountNumber = "3",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Balance = 5000,
                Status = AccountStatus.Live
            });

            var dataStoreServiceMock = new Mock<IDataStoreService>();
            dataStoreServiceMock.Setup(foo => foo.GetAccountDataStore()).Returns(dataStoreMock.Object);
            DataStore = dataStoreServiceMock.Object;

            var logServiceMock = new Mock<ILogService>();
            Logger = logServiceMock.Object;
        }

        [Test]
        public void TestBacsFails()
        {
            // Test if Bacs payment fails (unauthorized PaymentScheme)
            var request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "2",
                DebtorAccountNumber = "3",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Bacs
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestBacsSucceeds()
        {
            // Test if Bacs payment succeeds
            var request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "2",
                DebtorAccountNumber = "1",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Bacs
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestChapsFails()
        {
            // Test if Chaps payment fails (unauthorized payment scheme)
            var request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "1",
                DebtorAccountNumber = "2",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Chaps
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsFalse(result.Success);

            // Test if Chaps payment fails (wrong account status)
            request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "2",
                DebtorAccountNumber = "1",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Chaps
            };
            result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestChapsSucceeds()
        {
            // Test if Chaps payment succeeds
            var request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "2",
                DebtorAccountNumber = "3",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.Chaps
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void TestFasterPaymentsFails()
        {
            // Test if FasterPayments payment fails (insuficient balance)
            var request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "1",
                DebtorAccountNumber = "2",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.FasterPayments
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsFalse(result.Success);

            // Test if FasterPayments payment fails (unauthorized payment scheme)
            request = new MakePaymentRequest
            {
                Amount = 5001,
                CreditorAccountNumber = "1",
                DebtorAccountNumber = "3",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.FasterPayments
            };
            result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void TestFasterPaymentsSucceeds()
        {
            // Test if FasterPayments payment succeeds
            var request = new MakePaymentRequest
            {
                Amount = 100,
                CreditorAccountNumber = "1",
                DebtorAccountNumber = "2",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.FasterPayments
            };
            var result = PaymentService.MakePayment(request);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<MakePaymentResult>(result);
            Assert.IsTrue(result.Success);
        }
    }
}