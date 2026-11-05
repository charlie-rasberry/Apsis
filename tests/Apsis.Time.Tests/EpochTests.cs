namespace Apsis.Time.Tests;

public class EpochTests
{
    [Theory]
    [InlineData(2000,  1,  1, 12.0, 2451545.0, 211_813_488_000_000_000)]  // J2000
    [InlineData(1970,  1,  1,  0.0, 2440587.5, 210_866_760_000_000_000)]  // Unix epoch
    [InlineData(1858, 11, 17,  0.0, 2400000.5, 207_360_043_200_000_000)]  // MJD zero
    public void Anchors_HaveExactJulianDay(long y, long m, long d, double ut1, double expectedJd, long expectedUs) {
        var e = Epoch.JulianDayFromGregorian(y, m, d, ut1); 
        Assert.Equal(expectedUs, e.JulianMicroseconds);
        Assert.Equal(expectedJd, e.JulianDay, 9);
    }

    [Theory]
    [InlineData(1900,86_400_000_000)]   // not a leap year | 1 day
    [InlineData(2000, 172_800_000_000)] // is a leap year | 2 days
    public void CenturyLeapRule(long year, long expectedUs)
    {
        var feb28 = Epoch.JulianDayFromGregorian(year, 2, 28, 0.0); // not leap /100 not /400
        var mar01 = Epoch.JulianDayFromGregorian(year, 3, 1, 0.0); // leap: divisible by 400
        Assert.Equal(expectedUs, mar01.JulianMicroseconds - feb28.JulianMicroseconds);
    }

    [Theory]
    [InlineData(0.5 / 3600)]
    [InlineData(7.0 / 3)]
    [InlineData(23.999999)]
    [InlineData(13.7531)]
    [InlineData(1.0 / 3600)]
    public void FractionalUt1_IsExactToMicrosecond(double ut1)
    {
        var e = Epoch.JulianDayFromGregorian(2000, 1, 1, ut1);
        var expected = 2451544L * Duration.MicrosecondsPerDay 
                       + Duration.MicrosecondsPerDay / 2 
                       + Math.Round(ut1 * Duration.MicrosecondsPerHour);
        Assert.Equal(expected, e.JulianMicroseconds);
    }

    [Theory]
    [InlineData(1L, 31L)]
    [InlineData(2L, 28L)]
    [InlineData(3L, 31L)]
    [InlineData(4L, 30L)]
    public void MonthAdjustment_UsesTruncation(long month, long expectedDays)
    {
        var startingMonth = Epoch.JulianDayFromGregorian(2001, month, 1, 0.0);
        var laterMonth = Epoch.JulianDayFromGregorian(2001, month+1, 1, 0.0);
        var expected = Duration.MicrosecondsPerDay * expectedDays;
        Assert.Equal(expected, laterMonth.JulianMicroseconds - startingMonth.JulianMicroseconds);
    }
}