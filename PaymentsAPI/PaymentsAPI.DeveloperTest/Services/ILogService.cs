#region Usings

using System;
using ClearBank.DeveloperTest.Types;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    // Sample interface for brevity, only a couple of realistic methods included, 
    // in a real life scenario it could be a wrapper class that extended for example from log4net ILog with our own business uses
    public interface ILogService
    {
        void LogException(Exception e);
        void LogOperation(MakePaymentRequest request, MakePaymentResult result);
    }
}