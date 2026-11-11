namespace Apsis.Units;
using Utilities;

/// <summary>
/// An angle.
/// </summary>
/// <param name="Radians">The angle in radians.</param>
public readonly record struct Angle(double Radians)
{
    /// <summary>
    /// Create a new <see cref="Angle"/> instance from degrees
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static Angle FromDegrees(double degrees) => new(degrees.ToRadians());
    
    /// <summary>
    /// Create a new <see cref="Angle"/> instance from radians
    /// </summary>
    /// <param name="radians"></param>
    /// <returns></returns>
    public static Angle FromRadians(double radians) => new(radians);
    
    /// <summary>
    /// Get degrees derived from radians, not stored.
    /// </summary>
    public double Degrees => Radians.ToDegrees();
    
    /// <summary>
    /// Adds two <see cref="Angle"/> values.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Angle operator +(Angle a, Angle b) => new(a.Radians + b.Radians);
    
    /// <summary>
    /// Subtracts two <see cref="Angle"/> values.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Angle operator -(Angle a, Angle b) => new(a.Radians - b.Radians);
    
    /// <summary>
    /// Negates the specified angle.
    /// </summary>
    /// <param name="a">The angle to negate.</param>
    /// <returns>An angle with the same magnitude as <paramref name="a"/>, but with the opposite sign.</returns>
    public static Angle operator -(Angle a) => new(-a.Radians);

    /// <summary>
    /// Multiplies an <see cref="Angle"/> by a scalar
    /// </summary>
    /// <param name="a"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Angle operator *(Angle a, double scalar) => new(a.Radians * scalar);

    /// <summary>
    /// Multiplies a scalar by an <see cref="Angle"/>
    /// </summary>
    /// <param name="scalar"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Angle operator *(double scalar, Angle a) => new(scalar * a.Radians);

    /// <summary>
    /// Divides an <see cref="Angle"/> by a scalar
    /// </summary>
    /// <param name="a"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Angle operator /(Angle a, double scalar) => new(a.Radians / scalar);
}
