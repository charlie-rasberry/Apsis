using Apsis.Utilities;

namespace Apsis.Coordinates;
/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// </summary>
public record struct EcefCoordinate(long EcefXMicrometers, long EcefYMicrometers, long EcefZMicrometers) 
{
    /// <summary>
    /// ECEF x-axis value
    /// </summary>
    public long EcefXum => EcefXMicrometers;
    /// <summary>
    /// ECEF y-axis value
    /// </summary>
    public long EcefYum => EcefYMicrometers;
    /// <summary>
    /// ECEF z-axis value, through the North Pole
    /// </summary>
    public long EcefZum => EcefZMicrometers;
    
    /// <summary>
    /// Ecef X in meters
    /// </summary>
    public double EcefX => (double) EcefXMicrometers / MicrometersPerMeter;
    /// <summary>
    /// ECEF Y in meters 
    /// </summary>
    public double EcefY => (double) EcefYMicrometers / MicrometersPerMeter;
    /// <summary>
    /// ECEF Z in meters
    /// </summary>
    public double EcefZ => (double) EcefZMicrometers / MicrometersPerMeter;
    
    /// <summary>
    /// MicrometersPerMeter
    /// </summary>
    private const long MicrometersPerMeter = 1_000_000;
    /// <summary>
    /// Radius along the equator in meters
    /// </summary>
    private const double EquatorialRadiusA = 6_378_137.0;
    /// <summary>
    /// Radius along the North or South Pole in meters
    /// </summary>
    private const double PolarRadiusB = 6_356_752.3142;
    
    private const double EllipsoidFlatteningF = 1 - (PolarRadiusB / EquatorialRadiusA);
    private const double OneMinusF = 1 - EllipsoidFlatteningF; // the flattening ratio
    private const double SquareEccentricity = 1 - (PolarRadiusB * PolarRadiusB) / (EquatorialRadiusA * EquatorialRadiusA);
    

    
    /// <summary>
    /// Geodetic LLA to ECEF
    /// Give parameters as doubles in this case, shouldn't be frequently used, and also you should know the LLA rather than be calculating that
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="altitude"></param>
    /// <returns></returns>
    public static EcefCoordinate FromGeodeticLla(double latitude, double longitude, double altitude)
    {
        
        // Get radians / conversions
        // Get Prime Vertical Radius = N(phi)
        // Do the formula
        // Scale by Micrometers 1e6
        // Return the stored/scaled version
        longitude = ApsisMath.GetRadians(longitude); // Radians first
        latitude = ApsisMath.GetRadians(latitude); 
        var sinLat = Math.Sin(latitude);
        var squareSinLat = sinLat * sinLat;
        double cosLat = Math.Cos(latitude);
        double cosLong = Math.Cos(longitude);
        double primeVerticalRadius = PrimeVertical(squareSinLat);
        long xum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * cosLong) * MicrometersPerMeter));
        long yum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * Math.Sin(longitude)) * MicrometersPerMeter));
        long zum = (long) (Math.Round(((OneMinusF * OneMinusF) * primeVerticalRadius + altitude) * sinLat * MicrometersPerMeter));
        return new EcefCoordinate(xum, yum, zum);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squareSinLat"></param>
    /// <returns></returns>
    public static double PrimeVertical(double squareSinLat)
    {
        return EquatorialRadiusA / Math.Sqrt(1 - SquareEccentricity * squareSinLat);
    }
}