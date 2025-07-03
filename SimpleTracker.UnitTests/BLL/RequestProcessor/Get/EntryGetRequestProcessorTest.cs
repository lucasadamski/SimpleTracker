using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.UnitTests.Helper;
namespace SimpleTracker.UnitTests.BLL.RequestProcessor.Get
{
    public class EntryGetRequestProcessorTest
    {
        private Mock<ILogger> loggerMock = new Mock<ILogger>();
        private Mock<IEntrySqlDal> entrySqlDalMock = new Mock<IEntrySqlDal>();
        List<string> data =
            [
                "get",
                "entry"
            ];

        [Fact]
        public void Process_GetsNull_ReturnsEmptyList()
        {
            // Arrange
            data = null;
            var dalReturn = new List<Entry>();
            entrySqlDalMock.Setup(n => n.GetAllEntries(It.IsAny<string>())).Returns(dalReturn);

            var sut = new EntryGetRequestProcessor(loggerMock.Object, entrySqlDalMock.Object);

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
            List<Entry> dalReturn = null;
            entrySqlDalMock.Setup(n => n.GetAllEntries(It.IsAny<string>())).Returns(dalReturn);

            var sut = new EntryGetRequestProcessor(loggerMock.Object, entrySqlDalMock.Object);

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
            var dalReturn = ListGenerator.Generate<Entry>(5); ;
            entrySqlDalMock.Setup(n => n.GetAllEntries(It.IsAny<string>())).Returns(dalReturn);

            var sut = new EntryGetRequestProcessor(loggerMock.Object, entrySqlDalMock.Object);

            // Act
            var actualResult = sut.Process(data);

            // Assert
            actualResult.Should().BeOfType<List<string>>();
            actualResult.Count.Should().Be(5);
        }

    }
}
