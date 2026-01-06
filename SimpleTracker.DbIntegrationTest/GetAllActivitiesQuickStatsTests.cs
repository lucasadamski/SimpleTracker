using FakeItEasy;
using Serilog;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using static SimpleTracker.DbIntegrationTest.Configuration;
using FluentAssertions;



namespace SimpleTracker.DbIntegrationTest
{
    public class GetAllActivitiesQuickStatsTests
    {
        private ILogger logger;
        private SqlDataAccess sqlDataAccess;
        private TestDal testDal;
        private IEntryDal entryDal;
        private IActivityDal activityDal;

        private string name = "test";

        private readonly int _userId = 1;
        private readonly int _value = 50;
        private readonly int _activityId = 1;
        private readonly string _activityName = "push-ups";
        private readonly DateTime _currentDateTime = DateTime.UtcNow;

        public GetAllActivitiesQuickStatsTests()
        {
            logger = A.Fake<ILogger>();
            sqlDataAccess = new SqlDataAccess(TestDbConnectionString, logger);
            testDal = new TestDal(sqlDataAccess, logger);
            entryDal = new EntryDal(sqlDataAccess, logger);
            activityDal = new ActivityDal(sqlDataAccess, logger);
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
        public void WhenAddedOneEntryToday_ReturnsOneEntryForTodayWeekMonthAllTime()
        {
            // Arrange 
            PurgeAndPopulateDatabase();
            PurgeEntries();
            var testEntry = new Entry()
            {
                Value = _value,
                ActivityId = _activityId,
                DateAdded = _currentDateTime
            };
            entryDal.CreateNewEntry(testEntry);

            // Act 
            var actualResult = activityDal.GetAllActivitiesQuickStats(_userId);

            // Assert
            actualResult.Count().Should().Be(1);
            actualResult.Where(n => n.ActivityName == _activityName).First().TodayValue.Should().Be(_value);
            actualResult.Where(n => n.ActivityName == _activityName).First().ThisWeekValue.Should().Be(_value);
            actualResult.Where(n => n.ActivityName == _activityName).First().ThisMonthValue.Should().Be(_value);
            actualResult.Where(n => n.ActivityName == _activityName).First().AllTimeValue.Should().Be(_value);
        }


        [Fact]
        public void WhenAddedOneEntryInMonth_ReturnsOneEntryInAMonth()
        {
            // Arrange 
            PurgeAndPopulateDatabase();
            PurgeEntries();
            var testEntry = new Entry()
            {
                Value = _value,
                ActivityId = _activityId,
                DateAdded = _currentDateTime
            };
            entryDal.CreateNewEntry(testEntry);

            // Act 
            var actualResult = activityDal.GetAllActivitiesQuickStats(_userId);

            // Assert
            actualResult.Count().Should().Be(1);
            actualResult.Where(n => n.ActivityName == _activityName).First().ThisMonthValue.Should().Be(_value);
        }
    }
}
