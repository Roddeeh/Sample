using FluentAssertions;
using Sample.Application.Helpers;

namespace Sample_Tests.Helpers
{
    public class StaticsticHelper_
    {
       public void Should_CalculateAverageTradesPerMinute_With_Valid_Data()
        {
            //Arrange
            var count = 2;
            var minutes = 2;

            //Act
            var result = StatisticCalculationHelper.CalculateAverageTradesPerMinute(count, minutes);

            //Assert
            result.Should().Be(1);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public void Should_CalculateAverageTradesPerMinute_With_Invalid_Data(int count, double minutes)
        {
            //Act
            var result = StatisticCalculationHelper.CalculateAverageTradesPerMinute(count, minutes);

            //Assert
            result.Should().Be(0);
        }

        public void Should_CalculateAverageVolumePerMinute_With_Valid_Data()
        {
            //Arrange
            var volume = 2;
            var minutes = 2;

            //Act
            var result = StatisticCalculationHelper.CalculateAverageVolumePerMinute(volume, minutes);

            //Assert
            result.Should().Be(1);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public void Should_CalculateAverageVolumePerMinute_With_Invalid_Data(int count, double minutes)
        {
            //Act
            var result = StatisticCalculationHelper.CalculateAverageVolumePerMinute(count, minutes);

            //Assert
            result.Should().Be(0);
        }
    }
}
