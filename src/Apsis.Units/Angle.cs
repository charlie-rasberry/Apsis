namespace Apsis.Units;

/// <summary>
/// By default, returns the angle as a radian.
/// </summary>
/// <param name="Radians"></param>
public readonly record struct Angle(double Radians)
{
    public double Degrees => Radians *  (180 / Math.PI);
    /// <summary>
    /// Return the angle in degrees not radians
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static Angle FromDegrees(double degrees) => new(degrees * Math.PI / 180);
}

     
