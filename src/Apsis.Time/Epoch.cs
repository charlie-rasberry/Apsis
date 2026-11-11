namespace Apsis.Time;
/// <summary>
/// Epoch, the date to set as a reference / starting point
/// The Julian date (JD) of any instant is the Julian day number plus the fraction of a day since the preceding noon in Universal Time.
/// </summary>
public readonly record struct Epoch // Julian day whole and fraction
{
    /// <summary>
    /// Version of the julian day we store to avoid floating point precision issues
    /// </summary>
    public long JulianMicroseconds { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="julianMicroseconds"></param>
    public Epoch(long julianMicroseconds)
    {
        JulianMicroseconds = julianMicroseconds;
    }
    /// <summary>
    /// Full version of Julian day with decimals
    /// </summary>
    public double JulianDay => DescaleJulianDay(JulianMicroseconds);
    
    /// <summary>
    /// Converts and creates a julian day from a given gregorian calendar date
    /// Is accurate past year 2099, uses fliegel and van flandern 1968 proleptic gregorian calendar formula
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    /// <param name="ut1"></param>
    /// <returns></returns>
    public static Epoch JulianDayFromGregorian(long year, long month, long day, double ut1)
    {
        long jdn = GregorianDayNumber(year, month, day);
        var us = (long) (jdn * Duration.MicrosecondsPerDay
                    - Duration.MicrosecondsPerDay / 2
                    + Math.Round(ut1 * Duration.MicrosecondsPerHour)); 
        return new Epoch(us);
    }

    /// <summary>
    /// Julian day generated from microseconds, primarily for adding elapsed time to current julian day.
    /// Represents the Julian day relative to the days starting point.
    /// </summary>
    /// <param name="microseconds"></param>
    /// <returns></returns>
    public static Epoch ToJulianDayFromUs(long microseconds) // The stored version
    {
        return new Epoch(microseconds);
    }

    /// <summary>
    /// Returns full julian day epoch with the decimal as a string
    /// </summary>
    /// <param name="storedEpoch"></param>
    /// <returns></returns>
    private static double DescaleJulianDay(long storedEpoch)
    {
        return (double)storedEpoch / Duration.MicrosecondsPerDay;
    }

    private static long GregorianDayNumber(long year, long month, long day)
    {
        // Formula followed (Fliegel Van Flandern):
        // A = INT((M - 14) / 12)
        // JDN = INT(1461*(Y + 4800 + A)/4) + INT(367*(M - 2 - 12*A)/12) - INT(3*INT((Y + 4900 + A)/100)/4) + D - 32075
        // Calendar ADJUSTMENT factor
        long leapYearShift = (month - 14) / 12; // MUST truncate towards zero
        // Gregorian day number
        return (1461 * (year + 4800 + leapYearShift) / 4)
                               + (367 * (month - 2 - 12 * leapYearShift) / 12)
                               - (3 * ((year + 4900 + leapYearShift) / 100) / 4)
                               + day
                               - 32075;
    }

    /// <summary>
    /// Update the inputted epoch by a number of microseconds
    /// </summary>
    /// <param name="jd"></param>
    /// <param name="us"></param>
    /// <returns></returns>
    public static Epoch operator +(Epoch jd, Duration us)
    {
        return new Epoch(jd.JulianMicroseconds + us.Microseconds);
    }

}