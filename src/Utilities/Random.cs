namespace Utilities;
// Added this, but cannot confirm its working. I think this is needed as we will be using an older version. 
public static class RandomFix
{
    private static Random s_randomInstance = new();

    public static int Next(int min, int max)
    {
        if (s_randomInstance == null)
        {
            s_randomInstance = new Random(Guid.NewGuid().GetHashCode());
        }
        return s_randomInstance.Next(min, max);
    }

    public static double NextDouble()
    {
        if (s_randomInstance == null)
        {
            s_randomInstance = new Random(Guid.NewGuid().GetHashCode());
        }
        return s_randomInstance.NextDouble();
    }
}