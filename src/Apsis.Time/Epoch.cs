namespace Apsis.Time;
// 1970-01-01 00:00:00 UTC for Unix
// 2451545.0
// J = 2000 + (Julian date − 2451545.0) ÷ 365.25
// The Julian date (JD) of any instant is the Julian day number plus the fraction of a day since the preceding noon in Universal Time.
// 00:30:00.0 UT January 1, 2013, is 2456293.520833.
// 2550864.0 - Tuesday, 5 December 2271 at 12:00:00
/// <summary>
/// Julian Day Epoch
/// </summary>
public record struct Epoch(long JdW, long JdF) // Julian day whole and fraction
{
    private const long Scale = Duration.MicrosecondsPerDay * 1_000_000; // 86_400
    
    /// <summary>
    /// So we can access this in future if need be, can remove later if turns out we don't
    /// </summary>
    public readonly long JulianDayWhole => JdW;

    /// <summary>
    /// Same as above, I am not sure if this is entirely necessary but makes it easier to access later
    /// </summary>
    public readonly long JulianDayFraction => JdF;
    
    //double JulianDay => JdW +  JdF / Scale;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="ut1"></param>
    /// <returns></returns>
    public static Epoch JulianDayFromGregorian(long year, long month, long day, double ut1)
    {
        (long, long) jd = Apsis.Time.Clock.GregorianToJulian(year, month, day, ut1);
        return new Epoch(jd.Item1, jd.Item2);
    }

    /// <summary>
    /// Julian day generated from microseconds, primarily for adding elapsed time to current julian day.
    /// Represents the Julian day relative to the days starting point.
    /// </summary>
    /// <param name="microseconds"></param>
    /// <returns></returns>
    public static Epoch JulianDayFromUs(long microseconds)
    {
        long whole = microseconds / Duration.MicrosecondsPerDay;
        long usIntoDay = microseconds % Duration.MicrosecondsPerDay;
        Console.WriteLine($"usIntoDay: {usIntoDay}");
        // Subtraction
        if (usIntoDay < 0)
        {
            usIntoDay += Duration.MicrosecondsPerDay; whole--;
        }
        return new Epoch(whole, usIntoDay);
    }

    /// <summary>
    /// Returns full julian day epoch with the decimal as a string
    /// </summary>
    /// <param name="epoch"></param>
    /// <returns></returns>
    public static string EpochToString(Epoch epoch)
    {
        return $"{epoch.JulianDayWhole}.{epoch.JulianDayFraction}";
    }
}