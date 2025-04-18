using Xunit;
using FluentAssertions;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DTO;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.UnitTests.DAL
{
    public class ActivitySqlDalTest
    {
        [Fact]
        public void CreateNewActivity_TakesValidActivity_ReturnsSuccessTrue()
        {
          /*  // Arrange
            var expected = true;
            var temp = new NewEntryResult();

            var sqlDataAccessMock = new Mock<ISQLDataAccess>();

            sqlDataAccessMock.Setup(n => n.SaveData<It.IsAny<Activity>()>(It.IsAny<string>(), It.IsAny<It.IsAnyType>())).Returns(temp);

            var sampleActivity = new Activity() { Id = 1, Name = "sampleActivity", UnitId = 1 };

            var activitySqlDal = new ActivitySqlDal(sqlDataAccessMock.Object);



            // Act
            var actual = activitySqlDal.CreateNewActivity(sampleActivity);

            // Assert
            actual.Success.Should().Be(expected, "because both have same values");*/
        }
    }
} 