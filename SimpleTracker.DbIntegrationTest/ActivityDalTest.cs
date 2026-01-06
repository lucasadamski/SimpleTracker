using FakeItEasy;
using Serilog;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using FluentAssertions;
using SimpleTracker.DTO;
using static SimpleTracker.DbIntegrationTest.Configuration;


namespace SimpleTracker.DbIntegrationTest;

public class ActivityDalTest
{
    private ILogger logger;
    private SqlDataAccess sqlDataAccess;
    private TestDal testDal;
    private IActivityDal activityDal;

    private string name = "test";

    public ActivityDalTest()
    {
        logger = A.Fake<ILogger>();
        sqlDataAccess = new SqlDataAccess(TestDbConnectionString, logger);
        testDal = new TestDal(sqlDataAccess, logger);
        activityDal = new ActivityDal(sqlDataAccess, logger);
    }

    private void PurgeAndPopulateDatabase() => testDal.PurgeAndPopulateDatabase();
    
    // Create -------------------------------------------------

    [Fact]
    public void WhenCreatedActivity_Then_ReturnsAddedActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var activity = new Activity()
        {
            Name = name,
            UnitId = 1,
            UserId = UserId
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(4);
        actualResult.Reverse();
        actualResult.First().Name.Should().Be(name);
        actualResult.First().UserId.Should().Be(UserId);
        actualResult.First().UnitId.Should().Be(1);
    }

    [Fact]
    public void WhenCreatedNullActivity_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var activity = new Activity()
        {
            Name = name,
            UnitId = 1,
            UserId = UserId
        };
        activity = null;

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNullName_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var activity = new Activity()
        {
            Name = null,
            UnitId = 1,
            UserId = UserId
        };
      
        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNullUserId_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var activity = new Activity()
        {
            Name = name,
            UnitId = 1,
            UserId = -1
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    [Fact]
    public void WhenCreatedActivityWithNegativeUnitId_Then_ReturnsSameCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var activity = new Activity()
        {
            Name = name,
            UnitId = -1,
            UserId = UserId
        };

        // Act
        activityDal.CreateNewActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(3);
    }

    // Read ---------------------------------------------------

    [Fact]
    public void WhenReadsActivityById_Then_ReturnsActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = activityDal.ReadActivity(1, UserId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be(ActivityName1);
        actualResult.UserId.Should().Be(UserId);
        actualResult.UnitId.Should().Be(1);
    }

    [Fact]
    public void WhenReadsActivityByNonExistingId_Then_ReturnsEmptyActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = activityDal.ReadActivity(8, UserId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be("");
        actualResult.UserId.Should().Be(0);
        actualResult.UnitId.Should().Be(0);
    }

    [Fact]
    public void WhenReadsActivityByIdWithNullUserId_Then_ReturnsEmptyActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = activityDal.ReadActivity(1, -1);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Name.Should().Be("");
        actualResult.UserId.Should().Be(0);
        actualResult.UnitId.Should().Be(0);
    }

    // Update -------------------------------------------------
    [Fact]
    public void WhenUpdatedActivity_Then_ReturnsUpdatedActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var updatedName = "updatedName";
        var updatedUnitId = 2;
        var activity = new Activity()
        {
            Id = 3,
            Name = updatedName,
            UnitId = updatedUnitId,
            UserId = UserId
        };

        // Act
        var updateResult = activityDal.UpdateActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        updateResult.Should().BeTrue();
        actualResult.Count.Should().Be(3);
        actualResult.Reverse();
        actualResult.First().Name.Should().Be(updatedName);
        actualResult.First().UserId.Should().Be(UserId);
        actualResult.First().UnitId.Should().Be(updatedUnitId);
    }

    [Fact]
    public void WhenUpdatedActivityWithNullNameAndUserIdAndNegativeUnitId_Then_ReturnsFalse()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var updatedName = "updatedName";
        var updatedUnitId = 2;
        var activity = new Activity()
        {
            Id = 3,
            Name = null,
            UnitId = -1,
            UserId = -1
        };

        // Act
        var updateResult = activityDal.UpdateActivity(activity);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        updateResult.Should().BeFalse();
        actualResult.Count.Should().Be(3);
        actualResult.Reverse();
        actualResult.First().Name.Should().Be(ActivityName3);       // not modified, as in PopulateDatabase data
        actualResult.First().UserId.Should().Be(UserId);
        actualResult.First().UnitId.Should().Be(3);
    }


    // Delete -------------------------------------------------
    [Fact]
    public void WhenDeletesActivity_Then_DoesntReturnDeletedActivity()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        activityDal.DeleteActivity(1);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(2);
    }
    [Fact]
    public void WhenDeletesNonExistingIdActivity_Then_ReturnFalse()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var boolResult = activityDal.DeleteActivity(5);
        var actualResult = activityDal.GetAllActivities(UserId).ToList();

        // Assert
        actualResult.Count.Should().Be(3);
        boolResult.Should().BeFalse();
    }
}
