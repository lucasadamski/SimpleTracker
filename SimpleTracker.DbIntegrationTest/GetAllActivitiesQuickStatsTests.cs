using FakeItEasy;
using Microsoft.Extensions.Logging;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using static SimpleTracker.DbIntegrationTest.Configuration;


namespace SimpleTracker.DbIntegrationTest
{
    public class GetAllActivitiesQuickStatsTests
    {
        private ILogger logger;
        private SqlDataAccess sqlDataAccess;
        private TestDal testDal;
        private IEntryDal entryDal;

        private string name = "test";

        private readonly int _value = 50;
        private readonly int _activityId = 1;
        private readonly DateTime _currentDateTime = DateTime.UtcNow;

        public GetAllActivitiesQuickStatsTests()
        {
            logger = A.Fake<ILogger<SqlDataAccess>>();
            sqlDataAccess = new SqlDataAccess(TestDbConnectionString, logger);
            testDal = new TestDal(sqlDataAccess, logger);
            entryDal = new EntryDal(sqlDataAccess, logger);
        }

        private void PurgeAndPopulateDatabase()
        {
            testDal.PurgeAndPopulateDatabase();
        }

        private void PurgeEntries()
        {
            testDal.PurgeEntries();
        }

        [Fact]
        public void WhenAddedOneEntryInMonth_ReturnsOneEntryInAMonth()
        {
            // Arrange 
            // Act 
            // Assert

        }
    }
}
