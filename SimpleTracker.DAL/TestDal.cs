using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL
{
    public class TestDal : DalBase, ITestDal
    {
        public TestDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {
        }

        public bool PurgeAndPopulateDatabase()
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spPurgeAndPopulateDb]", new { });
            _logger.LogDebug("[dbo].[spPurgeDb] called");
            return result;
        }

        
    }
}
