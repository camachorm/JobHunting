#region Usings

using ClearBank.DeveloperTest.Data;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    public interface IDataStoreService
    {
        IAccountDataStore GetAccountDataStore();
    }
}