using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.UnitTests.Helper;


namespace SimpleTracker.UnitTests.BLL.RequestProcessor.Get
{
    public class SummaryGetRequestProcessorTest
    {
        private Mock<ILogger> loggerMock = new Mock<ILogger>();
        private Mock<ISummarySqlDal> summarySqlDalMock = new Mock<ISummarySqlDal>();
        List<string> data =
            [
                "get",
                "summary",
                "all-time"
            ];

        [Fact]
        public void Process_GetsNull_ReturnsEmptyList()
        {
            // Arrange
            data = null;
            var dalReturn = new List<string>();
            summarySqlDalMock.Setup(n => n.GetSummary()).Returns(dalReturn);

            var sut = new SummaryGetRequestProcessor(loggerMock.Object, summarySqlDalMock.Object);

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
            List<string> dalReturn = null;
            summarySqlDalMock.Setup(n => n.GetSummary()).Returns(dalReturn);

            var sut = new SummaryGetRequestProcessor(loggerMock.Object, summarySqlDalMock.Object);

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
            var dalReturn = ListGenerator.Generate(5); 
            summarySqlDalMock.Setup(n => n.GetSummary()).Returns(dalReturn);

            var sut = new SummaryGetRequestProcessor(loggerMock.Object, summarySqlDalMock.Object);

            // Act
            var actualResult = sut.Process(data);

            // Assert
            actualResult.Should().BeOfType<List<string>>();
            actualResult.Count.Should().Be(5);
        }
    }
}
