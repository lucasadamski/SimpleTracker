using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Factory;
using SimpleTracker.DTO;
using FluentAssertions;
using SimpleTracker.BLL.RequestProcessor.Get;

namespace SimpleTracker.UnitTests.BLL.Factory
{
    public class GetRequestProcessorFactoryTest
    {
        private Mock<ILogger> loggerMock = new Mock<ILogger>();

        [Fact]
        public void ReturnGetRequestProcessor_GetsValidInput_ReturnsCorrectFactory()
        {
            // Arrange
            var response = new Response()
            {
                RequestVerb = RequestVerb.Get,
                Type = typeof(Entry)
            };
            var getRequestProcessor = new GetRequestProcessorFactory(loggerMock.Object);
            var expectedResult = typeof(Entry);

            // Act
            var actualResult = getRequestProcessor.ReturnGetRequestProcessor(response);

            // Assert
            actualResult.Should().BeOfType<EntryGetRequestProcessor>();
        }
    }
}
