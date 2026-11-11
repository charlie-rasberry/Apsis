namespace Apsis.Units;

/// <summary>
/// Physical distance stored in metres 
/// </summary>
/// <param name="Metres"></param>
public readonly record struct Distance(double Metres)
{
    /// <summary>
    /// Distance in kilometres from metres
    /// </summary>
    public double Kilometres => Metres / 1000;
    
    /// <summary>
    /// Creates a new <see cref="Distance"/> instance from kilometres
    /// </summary>
    /// <param name="km"></param>
    /// <returns></returns>
    public static Distance FromKilometres(double km) => new Distance(km * 1000);
    
    /// <summary>
    /// Adds two <see cref="Distance"/> values
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Distance operator +(Distance a, Distance b) => new Distance(a.Metres + b.Metres);
    /// <summary>
    /// Subtracts two <see cref="Distance"/> values
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Distance operator -(Distance a, Distance b) => new Distance(a.Metres - b.Metres);
}