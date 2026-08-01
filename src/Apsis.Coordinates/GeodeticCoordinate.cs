namespace Apsis.Coordinates;

/// <summary>
/// Geodetic decimal coordinate for ground positions with elevation.
/// These coordinates are relative to the size, shape and gravity of the Earth.
/// </summary>
public readonly record struct GeodeticCoordinate
{
    /// <summary> Gets the geodetic latitude in decimal degrees. </summary>
    public readonly double GeodeticLatitude { get; init; }
    
    /// <summary> Gets the longitude in decimal degrees. </summary>
    public readonly double Longitude { get; init; }
    
    /// <summary> Gets the geodetic altitude in meters. </summary>
    public readonly double GeodeticAltitude { get; init; }
}