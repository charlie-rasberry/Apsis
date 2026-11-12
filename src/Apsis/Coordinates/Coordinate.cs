using System;
using Apsis.Utilities;

namespace Apsis.Coordinates;
/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// </summary>
public readonly record struct EcefCoordinate(long EcefXMicrometres, long EcefYMicrometres, long EcefZMicrometres) 
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
    /// WGS84 Semi-major axis (a), Radius along the equator in metres
    /// </summary>
    private const double EquatorialRadius = 6_378_137.0;
    
    /// <summary>
    /// Semi-minor axis (b), Radius along the North or South Pole in metres
    /// </summary>
    private const double PolarRadius = EquatorialRadius * (1 - Flattening);

    /// <summary>
    /// WGS84 Reciprocal of flattening, 1/f
    /// </summary>
    private const double ReciprocalOfFlattening = 298.257223563;
    
    /// <summary>
    /// Ellipsoid flattening derived from WGS84 reciprocal of flattening
    /// </summary>
    private const double Flattening = 1 / ReciprocalOfFlattening;
    
    private const double OneMinusF = 1 - Flattening; // the flattening ratio
    
    /// <summary>
    /// First eccentricity squared (e²)
    /// </summary>
    private const double SquareEccentricity = (2 * Flattening) - (Flattening * Flattening);
    

    
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
        longitude = longitude.ToRadians(); // Radians first
        latitude = latitude.ToRadians(); 
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
        return EquatorialRadius / Math.Sqrt(1 - SquareEccentricity * squareSinLat);
    }
}