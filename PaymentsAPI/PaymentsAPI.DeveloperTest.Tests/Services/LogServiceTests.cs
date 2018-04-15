#region Usings

using System;
using System.Diagnostics;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

#endregion

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class LogServiceTests
    {
        private const long MaxMilliseconds = 100;

        private long RunTimedDelegate(TestDelegate code)
        {
            var watch = Stopwatch.StartNew();
            code.Invoke();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
        // In real life we could be testing for more than null parameters
        // However a logging service's main responsibility is to not be disruptive to the callers
        // Neither by throwing exceptions or taking too long to return from the call

        // For that reason I chose to keep it simple around the fact it does not throw an exception,
        // And testing the execution times smaller than 100ms (Generous I know ;-) )

        // I would also like to highlight that although typically, performance testing is not part of the unit tests responsibility,
        // In this specific case (a logging service), it would be considered part of the passing criteria, 
        // hence me including a threshold test here

        [Test]
        public void LogExceptionTest()
        {
            var service = new LogService();
            Assert.DoesNotThrow(() => service.LogException(null));
            Assert.DoesNotThrow(() => service.LogException(new Exception("Sample exception")));
            Assert.Less(RunTimedDelegate(() => service.LogException(null)), MaxMilliseconds);
            Assert.Less(RunTimedDelegate(() => service.LogException(new Exception("Sample exception"))),
                MaxMilliseconds);
        }

        [Test]
        public void LogOperationTest()
        {
            var service = new LogService();

            // In real life we could be testing for more than null parameters
            Assert.DoesNotThrow(() => service.LogOperation(null, null));
            Assert.DoesNotThrow(() => service.LogOperation(new MakePaymentRequest(), null));
            Assert.DoesNotThrow(() => service.LogOperation(null, new MakePaymentResult()));
            Assert.DoesNotThrow(() => service.LogOperation(new MakePaymentRequest(), new MakePaymentResult()));
            Assert.Less(RunTimedDelegate(() => service.LogOperation(null, null)), MaxMilliseconds);
            Assert.Less(RunTimedDelegate(() => service.LogOperation(new MakePaymentRequest(), null)), MaxMilliseconds);
            Assert.Less(RunTimedDelegate(() => service.LogOperation(null, new MakePaymentResult())), MaxMilliseconds);
            Assert.Less(RunTimedDelegate(() => service.LogOperation(new MakePaymentRequest(), new MakePaymentResult())),
                MaxMilliseconds);
        }
    }
}