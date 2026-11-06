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
    
}