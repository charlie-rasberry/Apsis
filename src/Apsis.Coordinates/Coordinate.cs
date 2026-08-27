using Apsis.Utilities;

namespace Apsis.Coordinates;
/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// Max long size : 9,223,372,036,854,775,807
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

    private const long EquatorialRadiusAum = 637_813_700_000;
    private const long PolarRadiusBum = 635_675_231_420;

    private const double EllipsoidFlatteningF = 1 - (PolarRadiusB / EquatorialRadiusA);
    private const double OneMinusF = 1 - EllipsoidFlatteningF; // the flattening ratio
    private static double SquareNumericalEccentricityOfEllipsoidE => 1 - (Math.Pow(PolarRadiusB, 2) / Math.Pow(EquatorialRadiusA, 2)); 
    
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
        //      London, UK
        //      DD Latitude and longitude coordinates are: 51.509865, -0.118092
        //      ECEF x y z      [ 3977787.91, -8198.61, 4969054.24 ] 
        //      After testing: 3977787.910008 Y: -8198.60728  Z: 4969054.239987
        // X: 2026736474463283 Y: 1933185610758203  Z: 5988631442304806
        
        // Get radians / conversions
        // Get Prime Vertical Radius = N(phi)
        // Do the formula
        // Scale by Micrometers 1e6
        // Return the stored/scaled version
        longitude = Mathematical.GetRadians(longitude);
        latitude = Mathematical.GetRadians(latitude);
        double cosLat = Math.Cos(latitude);
        double cosLong = Math.Cos(longitude);
        double primeVerticalRadius = PrimeVertical(latitude);
        Console.WriteLine($"primeVerticalRadius: {primeVerticalRadius}");
        long xum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * cosLong) * MicrometersPerMeter));
        long yum = (long) (Math.Round(((primeVerticalRadius + altitude) * cosLat * Math.Sin(longitude)) * MicrometersPerMeter));
        // double Z = ((Math.Pow(EquatorialRadius, 2) / Math.Pow(PolarRadius, 2)) * PrimeVerticalRadius + altitude) * Math.Sin(longitude);
        // double Z = ((1 - (Math.Pow(EquatorialRadius, 2) / Math.Pow(PolarRadius, 2)) * PrimeVerticalRadius + altitude)) *
        //            Math.Sin(longitude);
        long zum = (long) (Math.Round(((OneMinusF * OneMinusF) * primeVerticalRadius + altitude) * Math.Sin(latitude) * MicrometersPerMeter));
        return new EcefCoordinate(xum, yum, zum);
    }

    private static void EcefToLla(in EcefCoordinate coordinate)
    {
        double x = coordinate.EcefX;
        double y = coordinate.EcefY;
        double z = coordinate.EcefZ;
        
        // We could use X = Math.Sqrt(Math.Pow(X, 2) * Math.Pow(Y, 2)), but we call it S in line with matlabs docs
        double s = Math.Sqrt(Math.Pow(x, 2) * Math.Pow(z, 2)); 
        
        // reduced latitude: an auxiliary angle used for mapping ellipsoidal earth position to a reference sphere
        double reducedLat = Math.Atan(z / (OneMinusF));
    }
    
    /// <summary>
    /// Not needed yet
    /// </summary>
    /// <param name="latitudeRadians"></param>
    /// <returns></returns>
    public static double GeocentricRadius(double latitudeRadians)
    {
        //latitude = GetRadians(latitude);
        return (Math.Sqrt((Math.Pow(Math.Pow(EquatorialRadiusA, 2) * Math.Cos(latitudeRadians), 2) + Math.Pow(Math.Pow(PolarRadiusB, 2) * Math.Sin(latitudeRadians), 2))
                          /(Math.Pow(EquatorialRadiusA * Math.Cos(latitudeRadians), 2) + Math.Pow(PolarRadiusB * Math.Sin(latitudeRadians), 2)))) * 1000;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="latitudeRadians"></param>
    /// <returns></returns>
    public static double PrimeVertical(double latitudeRadians)
    {
        return EquatorialRadiusA / Math.Sqrt(1 - SquareNumericalEccentricityOfEllipsoidE * Math.Pow(Math.Sin(latitudeRadians), 2));
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