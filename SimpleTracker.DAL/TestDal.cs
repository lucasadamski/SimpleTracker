using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL
{
    public class TestDal : DalBase, ITestDal
    {
        public TestDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {
        }

        public bool PurgeDatabase()
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spPurgeDb]", new { });
            _logger.LogDebug("[dbo].[spPurgeDb] called");
            return result;
        }

        public bool PopulateDatabase()
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spPopulateWithTestData]", new { });
            _logger.LogDebug("[dbo].[spPopulateWithTestData] called");
            return result;
        }
    }
}
