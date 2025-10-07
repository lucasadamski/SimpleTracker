using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using FluentAssertions;
using SimpleTracker.DTO;


namespace SimpleTracker.DbIntegrationTest;

public class DbIntegration // todo divide by domains, activity, entry etc 
{
    private ILogger logger;
    private SqlDataAccess sqlDataAccess;
    private TestDal testDal;
    private IActivityDal activityDal;

    private string name = "test";
    private string userId = "testUser";

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
            Name = name,
            UnitId = 1,
            UserId = userId
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(4);
        actualResult.Reverse();
        actualResult.First().Name.Should().Be(name);
        actualResult.First().UserId.Should().Be(userId);
        actualResult.First().UnitId.Should().Be(1);
    }

    [Fact]
    public void WhenDeletesActivity_Then_DoesntReturnDeletedActivity()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();

        // Act
        activityDal.DeleteActivity(1);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(2);
        actualResult.Reverse();
    }
}
