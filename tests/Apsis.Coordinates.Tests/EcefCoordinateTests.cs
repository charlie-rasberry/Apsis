namespace Apsis.Coordinates.Tests;

public class EcefCoordinateTests
{
    [Theory]
    [InlineData(0, 0, 0, 6378137, 0, 0)]            // exact. um: 6378137000000, 0, 0
    [InlineData(0, 0, 1000, 6379137, 0, 0)]         // exact. altitude adds along the normal
    [InlineData(0, 90, 0, 0, 6378137, 0)]           // exact
    [InlineData(0, 180, 0, -6378137, 0, 0)]         // exact
    [InlineData(90, 0, 0, 0, 0, 6356752.3142)]      // exact. um: 6356752314200  (= polar radius (b))
    [InlineData(-90, 0, 0, 0, 0, -6356752.3142)]    // exact
    public void GeodeticToEcef(double lat, double lon, double alt, double x, double y, double z)
    {
        var c = EcefCoordinate.FromGeodeticLla(lat, lon, alt);
        Assert.Equal(x, c.EcefX, 6);
        Assert.Equal(y, c.EcefY, 6);
        Assert.Equal(z, c.EcefZ, 6);
    }

    [Fact]
    public void London_MatchesReference_WithinOneCentimetre()
    {
        const double tolerance = 0.01;
        var c = EcefCoordinate.FromGeodeticLla(51.509865, -0.118092, 11.0); // don't use 0
        Assert.Equal(3977787.91, c.EcefX, tolerance);
        Assert.Equal(-8198.61, c.EcefY, tolerance);
        Assert.Equal(4969054.24, c.EcefZ, tolerance);
    }
    
}