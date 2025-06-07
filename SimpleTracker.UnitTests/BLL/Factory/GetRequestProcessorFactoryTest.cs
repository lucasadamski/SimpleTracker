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

        [Theory]
        [InlineData(typeof(Entry), typeof(EntryGetRequestProcessor))]
        [InlineData(typeof(Activity), typeof(ActivityGetRequestProcessor))]
        [InlineData(typeof(Summary), typeof(SummaryGetRequestProcessor))]
        [InlineData(typeof(int), typeof(UnknownGetRequestProcessor))]
        [InlineData(null, typeof(UnknownGetRequestProcessor))]
        public void ReturnGetRequestProcessor_GetsValidRequestType_ReturnsCorrectFactoryType(Type inputType, Type expectedResult)
        {
            // Arrange
            var response = new Response()
            {
                RequestVerb = RequestVerb.Get,
                Type = inputType
            };
            var getRequestProcessor = new GetRequestProcessorFactory(loggerMock.Object);

            // Act
            var actualResult = getRequestProcessor.ReturnGetRequestProcessor(response);

            // Assert
            actualResult.GetType().Should().Be(expectedResult);
        }

        [Fact]
        public void ReturnGetRequestProcessor_GetsCorruptedRequest_ReturnUnknownFactory()
        {
            // Arrange
            var getRequestProcessor = new GetRequestProcessorFactory(loggerMock.Object);
            var expectedResult = typeof(UnknownGetRequestProcessor);

            // Act
            var actualResult = getRequestProcessor.ReturnGetRequestProcessor(null);

            // Assert
            actualResult.GetType().Should().Be(expectedResult);
        }

    }
}
