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
    [InlineData(1_999_999, 1)]
    [InlineData(2_000_000, 2)]
    [InlineData(-1_999_999, -1)]
    public void Conversions_SecondsTruncateTowardZero(long us, long expected)
    {
        var duration = new Duration(us);
        Assert.Equal(expected, duration.Seconds);
    }
}