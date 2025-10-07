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

    // Create -------------------------------------------------

    [Fact]
    public void WhenCreatedActivity_Then_ReturnsAddedActivity()
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
    public void WhenCreatedNullActivity_Then_ReturnsSameCollection()
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
        activity = null;

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNullName_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();
        var activity = new Activity()
        {
            Name = null,
            UnitId = 1,
            UserId = userId
        };
      
        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNullUserId_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();
        var activity = new Activity()
        {
            Name = name,
            UnitId = 1,
            UserId = null
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNegativeUnitId_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();
        var activity = new Activity()
        {
            Name = name,
            UnitId = -1,
            UserId = userId
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    // Read ---------------------------------------------------

    [Fact]
    public void WhenReadsActivityById_Then_ReturnsActivity()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();

        // Act
        var actualResult = activityDal.ReadActivity(1, userId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be("push-ups");
        actualResult.UserId.Should().Be("testUser");
        actualResult.UnitId.Should().Be(1);
    }

    [Fact]
    public void WhenReadsActivityByNonExistingId_Then_ReturnsEmptyActivity()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();

        // Act
        var actualResult = activityDal.ReadActivity(8, userId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be("");
        actualResult.UserId.Should().Be("");
        actualResult.UnitId.Should().Be(0);
    }

    [Fact]
    public void WhenReadsActivityByIdWithNullUserId_Then_ReturnsEmptyActivity()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();

        // Act
        var actualResult = activityDal.ReadActivity(1, null);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be("");
        actualResult.UserId.Should().Be("");
        actualResult.UnitId.Should().Be(0);
    }

    // Update -------------------------------------------------

    // Delete -------------------------------------------------
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
    }
    [Fact]
    public void WhenDeletesNonExistingIdActivity_Then_ReturnFalse()
    {
        // Arrange
        PurgeDatabase();
        PopulateDatabase();

        // Act
        var boolResult = activityDal.DeleteActivity(5);
        var actualResult = activityDal.GetAllActivities("testUser").ToList();

        // Assert
        actualResult.Count.Should().Be(3);
        boolResult.Should().BeFalse();
    }
}
