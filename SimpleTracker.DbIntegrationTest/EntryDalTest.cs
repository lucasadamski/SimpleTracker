using FakeItEasy;
using Serilog;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using FluentAssertions;
using SimpleTracker.DTO;
using static SimpleTracker.DbIntegrationTest.Configuration;


namespace SimpleTracker.DbIntegrationTest;

public class EntryDalTest
{
    private ILogger logger;
    private SqlDataAccess sqlDataAccess;
    private TestDal testDal;
    private IEntryDal entryDal;

    private string name = "test";

    private readonly int _value = 50;
    private readonly int _activityId = 1;
    private readonly DateTime _currentDateTime = DateTime.UtcNow;

    public EntryDalTest()
    {
        logger = A.Fake<ILogger>();
        sqlDataAccess = new SqlDataAccess(TestDbConnectionString, logger);
        testDal = new TestDal(sqlDataAccess, logger);
        entryDal = new EntryDal(sqlDataAccess, logger);
    }

    private void PurgeAndPopulateDatabase()
    {
        testDal.PurgeAndPopulateDatabase();
    }


    // Create -------------------------------------------------

    [Fact]
    public void WhenCreatedEntry_Then_ReturnsAddedEntry()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var entry = new Entry()
        {
            Value = _value,
            ActivityId = _activityId,
            DateAdded = _currentDateTime
        };

        // Act
        entryDal.CreateNewEntry(entry);
        var actualResult = entryDal.ReadEntries(UserId).Data;

        // Assert
        actualResult.Count().Should().Be(4);
       // actualResult.Reverse();
        actualResult.Reverse().First().Value.Should().Be(_value);
        actualResult.Reverse().First().ActivityId.Should().Be(_activityId);
        actualResult.Reverse().First().DateAdded.Date.Should().Be(_currentDateTime.Date);
    }

    [Fact]
    public void WhenCreatedNullEntry_ThenDoesntAddEntry_ReturnsNotChangedCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var entry = new Entry();
        entry = null;

        // Act
        entryDal.CreateNewEntry(entry);
        var actualResult = entryDal.ReadEntries(UserId).Data;

        // Assert
        actualResult.Count().Should().Be(3);
    }

    [Fact]
    public void WhenCreatedEntryWithNegativeValue_ThenDoesntAddTheEntry_ReturnsUnmodifiedCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var entry = new Entry()
        {
            Value = -1,
            ActivityId = _activityId,
            DateAdded = _currentDateTime
        };

        // Act
        entryDal.CreateNewEntry(entry);
        var actualResult = entryDal.ReadEntries(UserId).Data;

        // Assert
        actualResult.Count().Should().Be(3);
    }

    // Read ---------------------------------------------------
    // one Entry
    [Fact]
    public void WhenReadsEntryWithNonExistingUserId_Then_ReturnsNoRecords()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = entryDal.ReadEntry(1, 13142);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeOfType<EntryEmpty>();
    }

    [Fact]
    public void WhenReadsEntryWithNullUserId_Then_ReturnsNoRecords()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = entryDal.ReadEntry(1, 0);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeOfType<EntryEmpty>();
    }

    [Fact]
    public void WhenReadsEntryById_Then_ReturnsEntry()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = entryDal.ReadEntry(1, UserId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Value.Should().Be(EntryValue1);
        actualResult.ActivityId.Should().Be(EntryActivityId1);
        actualResult.DateAdded.Should().Be(EntryDateAdded1);
    }

    [Fact]
    public void WhenReadsEntryByNegativeId_Then_ReturnsEmptyEntry()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var actualResult = entryDal.ReadEntry(-1, UserId);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeOfType<EntryEmpty>();
    }

    // multiple Entries

    [Fact]
    public void WhenReadsEntriesWithNonExistingUserId_Then_ReturnsEmptyCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2025, 5, 8, 12, 0, 0);
        var to = new DateTime(2025, 5, 8, 14, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(12124, from, to);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(0);
    }

    [Fact]
    public void WhenReadsEntriesWithNullUserId_Then_ReturnsEmptyCollection()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2025, 5, 8, 12, 0, 0);
        var to = new DateTime(2025, 5, 8, 14, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(0, from, to);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(0);
    }

    [Fact]
    public void WhenReadsEntriesByFromDateOnly_Then_ReturnsTwoRecords()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2025, 5, 8, 14, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(UserId, from, null);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(2);
    }

    [Fact]
    public void WhenReadsEntriesByFromAndToDate_Then_ReturnsTwoRecords()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2025, 5, 8, 12, 0, 0);
        var to = new DateTime(2025, 5, 9, 15, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(UserId, from, to);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(2);
    }

    [Fact]
    public void WhenReadsEntriesByToDateOnly_Then_ReturnsOneRecord()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2025, 5, 8, 12, 0, 0);
        var to = new DateTime(2025, 5, 8, 14, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(UserId, null, to);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(1);
        actualResult.Data.ElementAt(0).Should().BeOfType<Entry>();
    }

    [Fact]
    public void WhenReadsEntryFromFuture_Then_ReturnsNoRecords()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        
        var from = new DateTime(2027, 5, 8, 12, 0, 0);

        // Act
        var actualResult = entryDal.ReadEntries(UserId, from, null);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Data.Count().Should().Be(0);
    }

    // Update
    [Fact]
    public void WhenUpdatesEntry_Then_ReturnsUpdatedEntry()
    {
        // Arrange
        PurgeAndPopulateDatabase(); 
        

        // Act
        entryDal.UpdateEntry(1, 58);
        var actualResult = entryDal.ReadEntries(UserId);

        // Assert
        actualResult.Data.Count().Should().Be(3);
        actualResult.Data.ElementAt(0).Value.Should().Be(58);

    }


    // Delete -------------------------------------------------
    [Fact]
    public void WhenDeletesEntry_Then_DoesntReturnDeletedEntry()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        entryDal.DeleteEntry(1);
        var actualResult = entryDal.ReadEntries(UserId).Data;

        // Assert
        actualResult.Count().Should().Be(2);
    }
    [Fact]
    public void WhenDeletesNonExistingIdActivity_Then_ReturnFalse()
    {
        // Arrange
        PurgeAndPopulateDatabase();
        

        // Act
        var boolResult = entryDal.DeleteEntry(89);
        var actualResult = entryDal.ReadEntries(UserId).Data;
        // Assert
        actualResult.Count().Should().Be(3);
        boolResult.Should().BeFalse();
    }
}
