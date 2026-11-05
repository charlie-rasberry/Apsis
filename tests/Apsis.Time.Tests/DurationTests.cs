namespace Apsis.Time.Tests;

public class DurationTests
{
    [Fact]
    public void Constants_FormCorrectLadder()
    {
        Assert.Equal(1_000L, Duration.MicrosecondsPerMillisecond);
        Assert.Equal(1_000_000L, Duration.MicrosecondsPerSecond);
        Assert.Equal(60_000_000, Duration.MicrosecondsPerMinute);
        Assert.Equal(3_600_000_000, Duration.MicrosecondsPerHour);
        Assert.Equal(86_400_000_000, Duration.MicrosecondsPerDay);
        Assert.Equal(604_800_000_000, Duration.MicrosecondsPerWeek);
    }

    [Theory]
    // Small durations
    [InlineData(1_999_999, 1_999, 1, 0, 0, 0, 0)]
    [InlineData(2_000_000, 2_000, 2, 0, 0, 0, 0)]
    [InlineData(-1_999_999, -1_999, -1, 0, 0, 0, 0)]

    // Unit boundaries
    [InlineData(60_000_000, 60_000, 60, 1, 0, 0, 0)]
    [InlineData(3_600_000_000, 3_600_000, 3_600, 60, 1, 0, 0)]
    [InlineData(86_400_000_000, 86_400_000, 86_400, 1_440, 24, 1, 0)]
    [InlineData(604_800_000_000, 604_800_000, 604_800, 10_080, 168, 7, 1)]

    // Multiple units
    [InlineData(1_234_567_890_123, 1_234_567_890, 1_234_567, 20_576, 342, 14, 2)]
    [InlineData(2_419_200_000_000, 2_419_200_000, 2_419_200, 40_320, 672, 28, 4)]
    [InlineData(31_449_600_000_000, 31_449_600_000, 31_449_600, 524_160, 8_736, 364, 52)]

    // Negative durations
    [InlineData(-60_000_000, -60_000, -60, -1, 0, 0, 0)]
    [InlineData(-3_600_000_000, -3_600_000, -3_600, -60, -1, 0, 0)]
    [InlineData(-86_400_000_000, -86_400_000, -86_400, -1_440, -24, -1, 0)]
    [InlineData(-604_800_000_000, -604_800_000, -604_800, -10_080, -168, -7, -1)]
    public void Conversions_SecondsTruncateTowardZero(long us, long ms, long s, long m, long h, long d, long w)
    {
        var duration = new Duration(us);
        Assert.Equal(us, duration.Microseconds);
        Assert.Equal(ms, duration.Milliseconds);
        Assert.Equal(s, duration.Seconds);
        Assert.Equal(m, duration.Minutes);
        Assert.Equal(h, duration.Hours);
        Assert.Equal(d, duration.Days);
        Assert.Equal(w, duration.Weeks);
    }

    [Fact]
    public void Factories_RoundTripThroughProperties()
    {
        Assert.Equal(1_000L, Duration.FromMicroseconds(1_000).Microseconds);
        Assert.Equal(5_000L, Duration.FromMilliseconds(5).Microseconds);
        Assert.Equal(5_000_000L, Duration.FromSeconds(5).Microseconds);
        Assert.Equal(60_000_000L, Duration.FromMinutes(1).Microseconds);
        Assert.Equal(3_600_000_000L, Duration.FromHours(1).Microseconds);
        Assert.Equal(86_400_000_000L, Duration.FromDays(1).Microseconds);
        Assert.Equal(604_800_000_000L, Duration.FromWeeks(1).Microseconds);
    }

    [Fact]
    public void Operators_AddSubtractScale()
    {
        Assert.Equal(Duration.FromMicroseconds(2_000), Duration.FromMicroseconds(1_000) + Duration.FromMicroseconds(1_000));
        Assert.Equal(Duration.FromMicroseconds(3_000), Duration.FromMicroseconds(4_000) - Duration.FromMicroseconds(1_000));
        Assert.Equal(Duration.FromMicroseconds(5_000_000), Duration.FromMicroseconds(5_000) * 1_000L);
        Assert.Equal(Duration.FromMicroseconds(6_000),  Duration.FromMicroseconds(12_000) / 2);
    }
}