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
public record struct Epoch(long JulianDayWhole, long JulianDayFraction)
{
    /// <summary>
    /// 
    /// </summary>
    public readonly double JulianDay => JulianDayWhole +  JulianDayFraction / 10_000_000.0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="ut1"></param>
    /// <returns></returns>
    public static double JulianDayFromGregorian(long year, long month, long day, double ut1)
    {
        (long, long) jd = Apsis.Time.Clock.GregorianToJulian(year, month, day, ut1);
        return new Epoch(jd.Item1, jd.Item2).JulianDay;
    }
}