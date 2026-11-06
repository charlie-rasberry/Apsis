namespace Apsis.Utilities.Tests;

public class AngleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(90, Math.PI/2)]
    [InlineData(180, Math.PI)]
    [InlineData(360, 2*Math.PI)]
    [InlineData(-90, -Math.PI/2)]
    public void FromDegrees_ToRadians(double degrees, double radians)
    {
        Assert.Equal(radians, ApsisMath.GetRadians(degrees), 15);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(45)]
    [InlineData(90)]
    [InlineData(180)]
    [InlineData(359.999)]
    [InlineData(-45)]
    public void Degrees_IsInverseOfFromDegrees(double degrees)
    {
        Assert.Equal(degrees, ApsisMath.GetDegrees(ApsisMath.GetRadians(degrees)), 12);
    }
}