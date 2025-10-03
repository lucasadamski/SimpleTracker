using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using FluentAssertions;
using SimpleTracker.DTO;


namespace SimpleTracker.DbIntegrationTest;

public class DbIntegration
{
    private ILogger logger;
    private SqlDataAccess sqlDataAccess;
    private TestDal testDal;
    private IActivityDal activityDal;

    public DbIntegration()
    {
        logger = A.Fake<ILogger<SqlDataAccess>>();
        sqlDataAccess = new SqlDataAccess(@"Data Source=localhost;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False", logger);
        testDal = new TestDal(sqlDataAccess, logger);
        activityDal = new ActivityDal(sqlDataAccess, logger);
    }

    private void PurgeDatabase() => testDal.PurgeDatabase();
    
    private void PopulateDatabase() => testDal.PopulateDatabase();

    [Fact]
    public void WhenAddedActivity_Then_ReturnsAddedActivity()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();
        var activity = new Activity()
        {
            Name = "test",
            UnitId = 1,
            UserId = "test" // TODO make userid same as populated data
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities().ToList();

        // Assert
        actualResult.Should().Contain(activity);
    }
}
