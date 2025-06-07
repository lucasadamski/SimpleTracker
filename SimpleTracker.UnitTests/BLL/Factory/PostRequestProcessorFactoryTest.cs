using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Factory;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.BLL.RequestProcessor.Post;
using SimpleTracker.DTO;

namespace SimpleTracker.UnitTests.BLL.Factory
{
    public class PostRequestProcessorFactoryTest
    {
        private Mock<ILogger> loggerMock = new Mock<ILogger>();

        [Theory]
        [InlineData(typeof(Entry), typeof(EntryPostRequestProcessor))]
        [InlineData(typeof(Activity), typeof(ActivityPostRequestProcessor))]
        [InlineData(typeof(int), typeof(UnknownPostRequestProcessor))]
        [InlineData(null, typeof(UnknownPostRequestProcessor))]
        public void ReturnPostRequestProcessor_PostsValidRequestType_ReturnsCorrectFactoryType(Type inputType, Type expectedResult)
        {
            // Arrange
            var response = new Response()
            {
                RequestVerb = RequestVerb.Post,
                Type = inputType
            };
            var postRequestProcessor = new PostRequestProcessorFactory(loggerMock.Object);

            // Act
            var actualResult = postRequestProcessor.ReturnPostRequestProcessor(response);

            // Assert
            actualResult.GetType().Should().Be(expectedResult);
        }

        [Fact]
        public void ReturnPostRequestProcessor_PostsCorruptedRequest_ReturnUnknownFactory()
        {
            // Arrange
            var postRequestProcessor = new PostRequestProcessorFactory(loggerMock.Object);
            var expectedResult = typeof(UnknownPostRequestProcessor);

            // Act
            var actualResult = postRequestProcessor.ReturnPostRequestProcessor(null);

            // Assert
            actualResult.GetType().Should().Be(expectedResult);
        }
    }
}
