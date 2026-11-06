using Apsis.Utilities;

namespace Apsis.Coordinates;
/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// </summary>
public record struct EcefCoordinate(long EcefXMicrometres, long EcefYMicrometres, long EcefZMicrometres) 
{
    /// <summary>
    /// Ecef X in metres
    /// </summary>
    public double EcefX => (double) EcefXMicrometres / MicrometresPerMeter;
    /// <summary>
    /// ECEF Y in metres 
    /// </summary>
    public double EcefY => (double) EcefYMicrometres / MicrometresPerMeter;
    /// <summary>
    /// ECEF Z in metres
    /// </summary>
    public double EcefZ => (double) EcefZMicrometres / MicrometresPerMeter;
    
    /// <summary>
    /// MicrometresPerMeter
    /// </summary>
    private const long MicrometresPerMeter = 1_000_000;
    /// <summary>
    /// Radius along the equator in metres
    /// </summary>
    private const double EquatorialRadiusA = 6_378_137.0;
    /// <summary>
    /// Radius along the North or South Pole in metres
    /// </summary>
    private const double PolarRadiusB = 6_356_752.3142;
    
    private const double EllipsoidFlatteningF = 1 - (PolarRadiusB / EquatorialRadiusA);
    private const double OneMinusF = 1 - EllipsoidFlatteningF; // the flattening ratio
    private const double SquareEccentricity = 1 - (PolarRadiusB * PolarRadiusB) / (EquatorialRadiusA * EquatorialRadiusA);
    

    
    /// <summary>
    /// Geodetic LLA to ECEF
    /// Give parametres as doubles in this case, shouldn't be frequently used, and also you should know the LLA rather than be calculating that
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
        // Scale by Micrometres 1e6
        // Return the stored/scaled version
        longitude = ApsisMath.GetRadians(longitude); // Radians first
        latitude = ApsisMath.GetRadians(latitude); 
        var sinLat = Math.Sin(latitude);
        var squareSinLat = sinLat * sinLat;
        double cosLat = Math.Cos(latitude);
        double cosLong = Math.Cos(longitude);
        double primeVerticalRadius = PrimeVerticalRadius(squareSinLat);
        long xum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * cosLong) * MicrometresPerMeter));
        long yum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * Math.Sin(longitude)) * MicrometresPerMeter));
        long zum = (long) (Math.Round(((OneMinusF * OneMinusF) * primeVerticalRadius + altitude) * sinLat * MicrometresPerMeter));
        return new EcefCoordinate(xum, yum, zum);
    }

    /// <summary>
    /// Returns the prime vertical radius of curvature of earth at a given sin(latitude)^2 on the WGS 84 Ellipsoid 
    /// </summary>
    /// <param name="squareSinLat">The square of the sine of the geodetic latitude: sin(lat) * sin(lat).</param>
    /// <returns>The prime vertical radius of curvature in metres.</returns>
    public static double PrimeVerticalRadius(double squareSinLat)
    {
        return EquatorialRadiusA / Math.Sqrt(1 - SquareEccentricity * squareSinLat);
    }
}