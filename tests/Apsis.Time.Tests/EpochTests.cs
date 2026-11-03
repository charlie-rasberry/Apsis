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
}