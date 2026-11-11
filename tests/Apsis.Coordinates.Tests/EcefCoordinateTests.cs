namespace Apsis.Coordinates.Tests;

public class EcefCoordinateTests
{
    [Theory]
    [InlineData(0, 0, 0, 6378137, 0, 0)]            // exact. um: 6378137000000, 0, 0
    [InlineData(0, 0, 1000, 6379137, 0, 0)]         // exact. altitude adds along the normal
    [InlineData(0, 90, 0, 0, 6378137, 0)]           // exact
    [InlineData(0, 180, 0, -6378137, 0, 0)]         // exact
    [InlineData(90, 0, 0, 0, 0, 6356752.314245)]      // exact. um: 6356752314245.179  (= polar radius (b)) *NEW* Changed from 6356752314200 or 6356752.3142
    [InlineData(-90, 0, 0, 0, 0, -6356752.314245)]    // exact
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
        const double tolerance = 0.01; // works up to 1/2 cm
        var c = EcefCoordinate.FromGeodeticLla(51.509865, -0.118092, 11.0); // don't use 0
        Assert.Equal(3977787.91, c.EcefX, tolerance);
        Assert.Equal(-8198.61, c.EcefY, tolerance);
        Assert.Equal(4969054.24, c.EcefZ, tolerance);
    }

    [Fact]
    public void PrimeVertical_AtEquator_IsSemiMajorAxis()
    {
        Assert.Equal(6378137.0, EcefCoordinate.PrimeVerticalRadius(0), 6); // exact
    }
    
    [Fact]
    public void PrimeVertical_AtPole_IsASquaredOverB()
    {
Assert.Equal(6399593.625758, EcefCoordinate.PrimeVerticalRadius(Math.Sin(Math.PI / 2) * Math.Sin(Math.PI / 2)), 6); // = a^2/b = 6399593.62575849...    }
    }
    
    [Fact]
    public void MicrometreBackingIsExact()
    {
        Assert.Equal(6378137_000000L, EcefCoordinate.FromGeodeticLla(0,0,0).EcefXMicrometres);
    }
}