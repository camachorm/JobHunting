#region Usings

using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using NUnit.Framework;

#endregion

namespace ClearBank.DeveloperTest.Tests.Services
{
    /// <summary>
    ///     Simple as this testing suite may look, it guarantees the maintenance of the contract between the interface and the
    ///     expected results,
    ///     making it mandatory for easing code maintenance across time
    /// </summary>
    [TestFixture]
    class DataStoreServiceTests
    {
        private IDataStoreService GetDataStoreService(DataStoreService.DataStoreType dataStoreType)
        {
            return new DataStoreService(dataStoreType);
        }

        [Test]
        public void GetAccountDataStoreDoesNotReturnBackupDataStoreTest()
        {
            var result = GetDataStoreService(DataStoreService.DataStoreType.Live).GetAccountDataStore();
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOf<BackupAccountDataStore>(result);
            Assert.IsInstanceOf<AccountDataStore>(result);
        }

        [Test]
        public void GetAccountDataStoreDoesNotReturnLiveDataStoreTest()
        {
            var result = GetDataStoreService(DataStoreService.DataStoreType.Backup).GetAccountDataStore();
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOf<AccountDataStore>(result);
            Assert.IsInstanceOf<BackupAccountDataStore>(result);
        }
    }
}