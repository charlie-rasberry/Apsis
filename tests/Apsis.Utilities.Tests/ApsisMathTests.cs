namespace Apsis.Utilities.Tests;

public class ApsisMathTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(180, Math.PI)]
    [InlineData(-180, -Math.PI)]
    [InlineData(90, Math.PI/2)]
    public void GetRadians(double degrees, double radians)
    {
        Assert.Equal(radians, ApsisMath.GetRadians(degrees));
    }

    [Fact]
    public void Rad2DegFactor_IsReciprocalOfDeg2Rad()
    {
        Assert.Equal(57.29577951308232, ApsisMath.GetRadians(57.29577951308232) * ApsisMath.Rad2DegFactor); // 14D
    }
}