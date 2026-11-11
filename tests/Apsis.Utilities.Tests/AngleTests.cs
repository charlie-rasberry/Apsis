using Apsis.Units;

namespace Apsis.Utilities.Tests;

public class AngleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(90, Math.PI/2)]
    [InlineData(180, Math.PI)]
    [InlineData(360, 2*Math.PI)]
    [InlineData(-90, -Math.PI/2)]
    public void FromDegrees_Radians(double degrees, double radians)
    {
        var angle = Angle.FromDegrees(degrees);
        Assert.Equal(radians, angle.Radians, 15);
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(90, Math.PI/2)]
    [InlineData(180, Math.PI)]
    [InlineData(0, 2*Math.PI)]
    [InlineData(-90, -Math.PI/2)]
    public void FromRadians_Degrees(double degrees, double radians)
    {
        var angle = Angle.FromRadians(radians);
        Assert.Equal(degrees, angle.Degrees, 15);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(90)]
    [InlineData(180)]
    [InlineData(360)]
    [InlineData(-90)]
    public void FromDegrees_Degrees(double degrees)
    {
        var angle = Angle.FromDegrees(degrees);
        Assert.Equal(degrees, angle.Degrees, 15);
    }
    
    [Theory]
    [InlineData(0 ,0)]
    [InlineData(90, Math.PI/2)]
    [InlineData(180, Math.PI)]
    [InlineData(0, 2*Math.PI)]
    [InlineData(-90, -Math.PI/2)]
    public void FromRadians_Radians(double degrees, double radians)
    {
        var angle = Angle.FromRadians(radians);
        Assert.Equal(degrees.ToRadians(), angle.Radians, 15);
    }

    [Theory]
    [InlineData(0, 2 * Math.PI)]
    [InlineData(0, 4 * Math.PI)]
    [InlineData(90, 5 * Math.PI / 2)]
    [InlineData(45, 9 * Math.PI / 4)]
    [InlineData(180, 3 * Math.PI)]
    public void Wraparound_2PiPlus(double degrees, double radians)
    {
        var angle = Angle.FromRadians(radians);
        Assert.Equal(degrees, angle.Degrees, 15);
    }
}