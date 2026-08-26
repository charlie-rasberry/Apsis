namespace Apsis.Coordinates;

/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// Max long size : 9,223,372,036,854,775,807
/// </summary>
public record struct EcefCoordinate(long x, long y, long z)
{
    /// <summary>
    /// ECEF x-axis value
    /// </summary>
    public long Xum => x;
    /// <summary>
    /// ECEF y-axis value
    /// </summary>
    public long Yum => y;
    /// <summary>
    /// ECEF z-axis value, through the North Pole
    /// </summary>
    public long Zum => z;
    
    /// <summary>
    /// MicrometersPerMeter
    /// </summary>
    public const long MicrometersPerMeter = 1_000_000;

    
    public static EcefCoordinate FromGeodeticDecimalDegrees(double latitude, double longitude, double altitude)
    {
        //      London, UK
        //      DD Latitude and longitude coordinates are: 51.509865, -0.118092
        //      ECEF x y z [ 3977787.91, -8198.61, 4969054.24 ] 
        // Scale by Micrometers - 1e6
        // Do the formula
        // Return the stored/scaled version
        
        
    }

    /// <summary>
    /// Scales a given coordinate lat or long or altitude by micrometers per meter
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public static long ScaleCoordinate(double coordinate)
    {
        return (long)(coordinate * MicrometersPerMeter);
    }
}