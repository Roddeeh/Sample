using Sample.Application.Models;

namespace Sample.Application.Helpers
{
    public static class StatisticCalculationHelper
    {
        public static double CalculateAverageTradesPerMinute(int count, double totalMinutes)
        {
            if (count == 0 || totalMinutes == 0) 
                return 0;

            var averageTradesNumberPerMinute = count / totalMinutes;

            return averageTradesNumberPerMinute;
        }

        public static double CalculateAverageVolumePerMinute(double volume, double totalMinutes)
        {
            if (volume == 0 || totalMinutes == 0)
                return 0;

            var averageVolumePerMinute = volume / totalMinutes;

            return averageVolumePerMinute;
        }
    }
}
