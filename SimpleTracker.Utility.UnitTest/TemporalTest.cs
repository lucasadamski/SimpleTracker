using FakeItEasy;
using FluentAssertions;

namespace SimpleTracker.Utility.UnitTest
{
    public class TemporalTest
    {
        [Fact]
        public void ParseToDateTimeFrom_WhenTakesValidString_ThenReturnsDtObject()
        {
            // Arrange 
            var expectedResult = new DateTime(1, 1, 1, 12, 12, 12);
            var date = "01-01-0001 12:12:12";

            // Act
            var actualResult = Temporal.ParseToDateTime(date);


            // Assert
            actualResult.Value.Year.Should().Be(expectedResult.Year);
            actualResult.Value.Day.Should().Be(expectedResult.Day);
            actualResult.Value.Month.Should().Be(expectedResult.Month);

            actualResult.Value.Hour.Should().Be(expectedResult.Hour);
            actualResult.Value.Minute.Should().Be(expectedResult.Minute);
            actualResult.Value.Second.Should().Be(expectedResult.Second);


        }
    }
}