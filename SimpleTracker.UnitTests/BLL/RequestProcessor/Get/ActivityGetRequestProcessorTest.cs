using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.UnitTests.Helper;

namespace SimpleTracker.UnitTests.BLL.RequestProcessor.Get
{
    public class ActivityGetRequestProcessorTest
    {
        private Mock<ILogger> loggerMock = new Mock<ILogger>();
        private Mock<IActivitySqlDal> activitySqlDalMock = new Mock<IActivitySqlDal>();

        [Fact]
        public void Process_GetsNull_ReturnsEmptyList()
        {
            // Arrange
            List<string> data = null;
            var dalReturn = new List<Activity>();
            activitySqlDalMock.Setup(n => n.GetAllActivities()).Returns(dalReturn);

            var sut = new ActivityGetRequestProcessor(loggerMock.Object, activitySqlDalMock.Object);

            // Act
            var actualResult = sut.Process(data);

            // Assert
            actualResult.Should().BeOfType<List<string>>();
            actualResult.Count.Should().Be(0);
        }

        [Fact]
        public void Process_DalReturnsNull_ReturnsEmptyList()
        {
            // Arrange
            var data = new List<string>();
            List<Activity> dalReturn = null;
            activitySqlDalMock.Setup(n => n.GetAllActivities()).Returns(dalReturn);

            var sut = new ActivityGetRequestProcessor(loggerMock.Object, activitySqlDalMock.Object);

            // Act
            var actualResult = sut.Process(data);

            // Assert
            actualResult.Should().BeOfType<List<string>>();
            actualResult.Count.Should().Be(0);
        }

        [Fact]
        public void Process_DalReturns5Records_Returns5ElementsList()
        {
            // Arrange
            var data = ListGenerator.Generate(5);
            var dalReturn = ListGenerator.Generate<Activity>(5); ;
            activitySqlDalMock.Setup(n => n.GetAllActivities()).Returns(dalReturn);

            var sut = new ActivityGetRequestProcessor(loggerMock.Object, activitySqlDalMock.Object);
            
            // Act
            var actualResult = sut.Process(data);

            // Assert
            actualResult.Should().BeOfType<List<string>>();
            actualResult.Count.Should().Be(5);
        }
    }
}
