namespace Apsis.Units.Tests;

public class DistanceTests
{
    [Theory]
    [InlineData(1000, 1)]
    [InlineData(1, 0.001)]
    [InlineData(-500, -0.5)]
    public void Kilometres_IsMetresOverThousand(double metres, double kilometres)
    {
        var d =  new Distance(metres);
        Assert.Equal(kilometres, d.Kilometres);
    }

    [Fact]
    public void FromKilometres_RoundTrips()
    {
        Assert.Equal(2.5, Distance.FromKilometres(2.5).Kilometres);
    }

    [Fact]
    public void Operators_AddSubtract()
    {   
        var a = new Distance(10250);
        var b = Distance.FromKilometres(1.5);
        Assert.Equal(11750, (a + b).Metres);
        Assert.Equal(-8750, (b - a).Metres);
    }
}