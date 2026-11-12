using System.Diagnostics;

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

    [Fact]
    public void Ratio_ReturnsDouble()
    {
        Assert.Equal(2.0, Duration.FromHours(1) / Duration.FromMinutes(30));
    }

    [Theory] 
    [InlineData(100_000_000, true, 9_000_000_000_000, true)]
    public void Comparisons(long us1, bool expected1, long us2, bool expected2)
    {
        Assert.Equal(expected1, Duration.FromMicroseconds(1_000_000) < Duration.FromMicroseconds(us1)
                         && Duration.FromMicroseconds(1_000_000) <= Duration.FromMicroseconds(us1)
                         && Duration.FromMicroseconds(1_000_000_000) > Duration.FromMicroseconds(us1)
                         && Duration.FromMicroseconds(1_000_000_000) >= Duration.FromMicroseconds(us1)
                         && Duration.FromMicroseconds(1_000_000_000_000) != Duration.FromMicroseconds(us1)); // Not comparing us directly, not implemented by me
        Assert.Equal(expected2, Duration.FromMicroseconds(9_000_000_000_000) >= Duration.FromMicroseconds(us2));
    }

    [Fact]
    public void Deconstruct_YieldsUsAndMs()
    {
        var (us, ms) = (Duration.FromMilliseconds(1500));
        Assert.Equal(1_500_000L, us);
        Assert.Equal(1_500L, ms);
    }

    [Theory]
    [InlineData(1_500_000, "1.500000s")]
    [InlineData(-1_500_000, "-1.500000s")]
    [InlineData(-500_000,  "-0.500000s")]
    [InlineData(0,"0.000000s")]
    [InlineData(-1,"-0.000001s")]
    public void ToStringHandlesNegatives(long us, string expected)
    {
        var duration = new Duration(us);
        Assert.Equal(expected, duration.ToString());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3600)]
    [InlineData(10_000)]
    [InlineData(86_400)]
    [InlineData(9_223_372_036)] // Max value before overflow for linux: Stopwatch.Frequency * long.MaxValue
    public void FromStopwatchTick_DoesNotOverflow(long seconds)
    {
        var ticks = seconds * Stopwatch.Frequency;
        Assert.Equal(seconds * 1_000_000, Duration.FromStopwatchTick(ticks).Microseconds);
    }

    [Fact]
    public void Zero_IsDefault()
    {
        var duration = default(Duration);
        Assert.Equal(Duration.Zero, duration);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-3600)]
    public void Zero_DurationPlusZero(long seconds)
    {
        var duration = Duration.FromSeconds(seconds);
        Assert.Equal(duration, duration + Duration.Zero);
    }

    [Fact]
    public void Negate_Zero_ReturnsZero()
    {
        Assert.Equal(Duration.Zero, -Duration.Zero);
    }

    [Theory]
    [InlineData(1, -1_000_000)]
    [InlineData(-1, 1_000_000)]
    public void Negate_Seconds(long seconds, long negatedMicroseconds)
    {
        var duration = Duration.FromSeconds(seconds);
        duration = -duration;
        Assert.Equal(negatedMicroseconds, duration.Microseconds);
    }

    [Theory]
    [InlineData(1_500_000, 1.5)]
    [InlineData(-1_500_000, -1.5)]
    [InlineData(-1_500_000_000_000, -1_500_000
    )]
    public void TotalSeconds_FromMicroseconds(long microseconds, double expected)
    {
        var duration =  Duration.FromMicroseconds(microseconds);
        Assert.Equal(expected, duration.TotalSeconds);
    }
    
    [Theory]
    [InlineData(1_500_000, 1)]
    [InlineData(-1_500_000, -1)]
    [InlineData(-1_500_000_000_000, -1_500_000)]
    [InlineData(-500_000, 0)]
    public void Seconds_Truncates_FromMicroseconds(long microseconds, double expected)
    {
        var duration =  Duration.FromMicroseconds(microseconds);
        Assert.Equal(expected, duration.Seconds);
    }
    
    [Theory]
    [InlineData(-1_000_000,-1)]
    [InlineData(0,0)]
    [InlineData(500_000, 1)]
    [InlineData(1_500_000, 1)]
    public void CompareTo_LessThanZero(long microseconds, int expected)
    {
        var duration = Duration.FromMicroseconds(microseconds);
        Assert.Equal(expected, duration.CompareTo(Duration.Zero));
    }

    [Fact]
    public void CompareTo_MicrosecondsMaxValue()
    {
        Assert.Equal(1, Duration.FromMicroseconds(long.MaxValue).CompareTo(Duration.FromMicroseconds(-1)));
        Assert.Equal(-1, Duration.FromMicroseconds(-long.MaxValue).CompareTo(Duration.FromMicroseconds(1)));
        Assert.Equal(0, Duration.FromMicroseconds(long.MaxValue).CompareTo(Duration.FromMicroseconds(long.MaxValue)));
        Assert.Equal(-1, Duration.FromMicroseconds(long.MinValue).CompareTo(Duration.FromMicroseconds(1)));
    }
}