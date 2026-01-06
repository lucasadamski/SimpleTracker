using Serilog;
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
            _logger.Debug("[dbo].[spPurgeDb] called");
            return result;
        }

        public bool PurgeEntries()
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spPurgeEntries]", new { });
            _logger.Debug("[dbo].[spPurgeEntries] called");
            return result;
        }


    }
}
