namespace Apsis.Utilities;
/// <summary>
/// 
/// </summary>
public static class RandomFix
{
    private static Random s_randomInstance = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Next(int min, int max)
    {
        s_randomInstance = new Random(Guid.NewGuid().GetHashCode());
        return s_randomInstance.Next(min, max);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static double NextDouble()
    {
        s_randomInstance = new Random(Guid.NewGuid().GetHashCode());
        return s_randomInstance.NextDouble();
    }
}