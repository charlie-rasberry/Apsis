namespace Apsis.Utilities;

/// <summary>
/// 
/// </summary>
public static class Mathematical
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly double Deg2Rad = Math.PI / 180;
    /// <summary>
    /// 
    /// </summary>
    public static readonly double Rad2Deg = 180.0 / Math.PI;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double GetRadians (double degrees) => degrees * Deg2Rad;
    
}