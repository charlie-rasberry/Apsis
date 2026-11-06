namespace Apsis.Units;

public readonly record struct Distance(double Metres)
{
    /// <summary>
    /// Kilometres KM converted from metres M
    /// </summary>
    public double Kilometres => Metres / 1000;
    /// <summary>
    /// Returns metres M from kilometres KM
    /// </summary>
    /// <param name="km"></param>
    /// <returns></returns>
    public static Distance FromKilometres(double km) => new Distance(km * 1000);
    
    /// <summary>
    /// Operator overload for adding a distance in M metres
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Distance operator +(Distance a, Distance b) => new Distance(a.Metres + b.Metres);
    /// <summary>
    /// Operator overload for subtracting a distance in M metres
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Distance operator -(Distance a, Distance b) => new Distance(a.Metres - b.Metres);
}