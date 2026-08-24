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
public record struct Epoch(long Jd) // Julian day whole and fraction
{
    private const long ScaleUsPerDay = Duration.MicrosecondsPerDay;
    /// <summary>
    /// Converts and creates a julian day from a given gregorian calendar date
    /// Is not accurate past a certain year in the future... it might / probably is 2100
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <param name="ut1"></param>
    /// <returns></returns>
    public static long JulianDayFromGregorian(long year, long month, long day, double ut1)
    {
        double jdScaled = GregorianToJulian(year, month, day, ut1) * ScaleUsPerDay;
        return (long) jdScaled ;
    }

    /// <summary>
    /// Julian day generated from microseconds, primarily for adding elapsed time to current julian day.
    /// Represents the Julian day relative to the days starting point.
    /// </summary>
    /// <param name="microseconds"></param>
    /// <returns></returns>
    public static long ToJulianDayFromUs(long microseconds) // The stored version
    {
        return microseconds /= Duration.MicrosecondsPerDay;
    }

    /// <summary>
    /// Returns full julian day epoch with the decimal as a string
    /// </summary>
    /// <param name="storedEpoch"></param>
    /// <returns></returns>
    public static double StoredJulianDayToDisplayJulianDay(long storedEpoch)
    {
        return (double) storedEpoch / Duration.MicrosecondsPerDay;
    }

    /// <summary>
    /// 
    /// D: Day of the month (e.g., 1 to 31).
    /// M: Month of the year (e.g., 1 for January, 12 for December).
    /// Y: Year (e.g., 2026).
    /// 
    /// 14 and 12: Used together in INT((M - 14) / 12) to force January (M=1) and February (M=2) to result in -1.
    /// For all other months, it results in 0.
    /// 
    /// 2 and 12: Used in M - 2 - 12*A to re-index the months so that March becomes month 1 and February becomes month 12.
    /// This places the "problematic" leap day (February 29) at the very end of the mathematical year.
    ///
    /// 1461: The exact number of days in a standard 4-year Julian cycle (\(365 \times 3 + 366 = 1461\)).
    /// 4: Divided into the total to distribute those days across years, inherently accounting for the standard Julian leap year every 4 years.
    ///
    /// 367 and 12: The fraction 367/12 equals 30.583. When multiplied by the shifted month number and passed through, it perfectly replicates the alternating 30 and 31-day sequence of the calendar from March through January without needing an internal calendar database.
    ///
    /// 100: Represents a century. The Gregorian calendar omits leap years on century years unless divisible by 400.3 and 4: The term INT(3 * INT(...) / 4) strips out exactly 3 leap days every 400 years (e.g., dropping them in 1700, 1800, and 1900, but keeping it in 2000) to keep the formula tracking the modern Gregorian calendar instead of the older Julian calendar.
    ///
    /// 4800 and 4900: Added to the year variables to ensure that all intermediate calculations remain strictly positive numbers, even when calculating ancient historical BC dates. This prevents modern computer processors from mishandling negative integer division truncation.32075: The final offset correction. It subtracts the excess days introduced by adding 4800/4900 years, cleanly aligning the final total to day 0 on the exact day the Julian Period began.
    /// 
    /// </summary>
    /// <returns></returns>
    private static double GregorianToJulian(long year, long month, long day, double ut1)
    {
        // A = INT((M - 14) / 12)
        // JDN = INT(1461*(Y + 4800 + A)/4) + INT(367*(M - 2 - 12*A)/12) - INT(3*INT((Y + 4900 + A)/100)/4) + D - 32075
        
        // Calendar ADJUSTMENT factor
        long adjustmentFactor = (month - 14) / 12;
        
        // Julian Day NUMBER
        long julianDayNumber = (1461 * (year + 4800 + adjustmentFactor) / 4)
                               + (367 * (month - 2 - 12 * adjustmentFactor) / 12)
                               - (3 * ((year + 4900 + adjustmentFactor) / 100) / 4)
                               + day
                               - 32075;
        Console.WriteLine($"DEBUG: {julianDayNumber}{(0.5 + ut1/24)}");
        return julianDayNumber - 0.5 + ut1 / 24.0;
        // equivalent of (ut1 >= 12 ? 1 : 0) + (((ut1 + 12.0) % 24.0) / 24.0)    is    0.5 + ut1/24
    }
}