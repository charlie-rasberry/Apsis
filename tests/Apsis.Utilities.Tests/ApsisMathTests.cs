namespace Apsis.Utilities.Tests;

public class ApsisMathTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(180, Math.PI)]
    [InlineData(-180, -Math.PI)]
    [InlineData(90, Math.PI/2)]
    public void ToRadians(double degrees, double radians)
    {
        Assert.Equal(radians, degrees.ToRadians());
    }

    [Fact]
    public void Rad2DegFactor_IsReciprocalOfDeg2Rad()
    {
        Assert.Equal(57.29577951308232, 57.29577951308232.ToRadians() * ApsisMath.Rad2DegFactor); // 14D
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
        Assert.Equal(degrees, degrees.ToRadians().ToDegrees(), 12);
    }
}