namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        public Stats Calculate(string semicolonDelimitedContributions)
        {
            // TODO: your implementation
            Stats stats = new Stats();
            string[] contributions = semicolonDelimitedContributions.Split(';', StringSplitOptions.TrimEntries);
            if (contributions.Length > 0)
            {
                double[] contributedNumbers = contributions.
                     Select(x => double.TryParse(x.Replace("$", string.Empty), out double parsedNumber) ? parsedNumber : 0)
                     .Where(x => x > 0)
                    .ToArray();
                if (contributedNumbers.Length > 0)
                {
                    //sort the numbers
                    contributedNumbers = contributedNumbers.OrderBy(item => item).ToArray();

                    int length = contributedNumbers.Length;
                    int middleIndex = length / 2;

                    double median;

                    if (length % 2 == 0)
                    {
                        // If the number of elements is even, calculate the average of the two middle elements
                        median = (contributedNumbers[middleIndex - 1] + contributedNumbers[middleIndex]) / 2.0;
                    }
                    else
                    {
                        // If the number of elements is odd, the median is the middle element
                        median = contributedNumbers[middleIndex];
                    }

                    // Find the median      
                    stats.Median = Math.Round(median, 2);

                    // Find the range
                    stats.Range = Math.Round(contributedNumbers[^1] - contributedNumbers[0], 2);

                    // Find the average
                    stats.Average = Math.Round(contributedNumbers.Average(), 2);
                }
            }
            return stats;
        }
    }
}
