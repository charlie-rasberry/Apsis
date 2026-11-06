namespace Apsis.Utilities;

/// <summary>
/// 
/// </summary>
public static class ApsisMath
{
    /// <summary>
    /// 
    /// </summary>
    private const double Deg2RadFactor = Math.PI / 180;

    /// <summary>
    /// 
    /// </summary>
    public const double Rad2DegFactor = 180.0 / Math.PI;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double GetRadians (double degrees) => degrees * Deg2RadFactor; // change to use "this double degrees"
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="radians"></param>
    /// <returns></returns>
    public static double GetDegrees (double radians) => radians * Rad2DegFactor;
    
}