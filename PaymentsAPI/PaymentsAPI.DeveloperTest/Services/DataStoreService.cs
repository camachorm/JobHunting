#region Usings

using System;
using System.Configuration;
using ClearBank.DeveloperTest.Data;

#endregion

namespace ClearBank.DeveloperTest.Services
{
    public class DataStoreService : IDataStoreService
    {
        public enum DataStoreType
        {
            Backup,
            Live
        }

        public DataStoreService() : this(ConfigurationManager.AppSettings["DataStoreType"])
        {
        }

        public DataStoreService(DataStoreType type) : this(Enum.GetName(typeof(DataStoreType), type))
        {
        }

        private DataStoreService(string dataStoreType)
        {
            StoreType = dataStoreType;
        }

        private string StoreType { get; }

        public IAccountDataStore GetAccountDataStore()
        {
            return StoreType != "Backup"
                ? (IAccountDataStore) new AccountDataStore()
                : new BackupAccountDataStore();
        }
    }
}