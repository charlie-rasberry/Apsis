namespace Apsis.Coordinates;

/// <summary>
/// ECEF is the primary value
/// Geodetic Longitude Latitude Altitude are derived from ECEF
/// GeodeticLatitude
/// Longitude
/// GeodeticAltitude
/// </summary>
public readonly record struct Coordinate(long ecef_x, long ecef_y, long ecef_z);
{
    
    
    
}