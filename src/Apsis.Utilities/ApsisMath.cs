namespace Apsis.Utilities;

/// <summary>
/// 
/// </summary>
public static class ApsisMath
{
    /// <summary>
    /// 
    /// </summary>
    public const double Deg2RadFactor = Math.PI / 180;

    /// <summary>
    /// 
    /// </summary>
    public const double Rad2DegFactor = 180.0 / Math.PI;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public static double ToRadians(this double degrees) => degrees * Deg2RadFactor;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="radians"></param>
    /// <returns></returns>
    public static double ToDegrees(this double radians) => radians * Rad2DegFactor;
    
}