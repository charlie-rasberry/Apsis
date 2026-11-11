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
    
    /// <summary>
    /// Calculates the Julian Day Number (JDN) from a Gregorian calendar date using the Fliegel and Van Flandern algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implements the standard integer-division formula:
    /// <c>JDN = (1461 * (Y + 4800 + A) / 4) + (367 * (M - 2 - 12 * A) / 12) - (3 * ((Y + 4900 + A) / 100) / 4) + D - 32075</c>
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <term><c>14</c> and <c>12</c> (Month Shift):</term>
    /// <description>Used in <c>(M - 14) / 12</c> to force January (<c>1</c>) and February (<c>2</c>) to evaluate to <c>-1</c>, while all other months evaluate to <c>0</c>.</description>
    /// </item>
    /// <item>
    /// <term><c>2</c> and <c>12</c> (Calendar Re-indexing):</term>
    /// <description>Used in <c>M - 2 - 12 * A</c> to shift the year start to March (month <c>1</c>) and push February to month <c>12</c>, placing leap days at the very end of the mathematical year.</description>
    /// </item>
    /// <item>
    /// <term><c>1461</c> and <c>4</c> (Julian Cycle):</term>
    /// <description><c>1461</c> is the exact number of days in a 4-year cycle (<c>365 * 3 + 366</c>). Dividing by <c>4</c> distributes days across years and automatically adds a leap day every 4 years.</description>
    /// </item>
    /// <item>
    /// <term><c>367</c> and <c>12</c> (Month Length Approximation):</term>
    /// <description>The ratio <c>367 / 12</c> (<c>30.583</c>) replicates the alternating 30- and 31-day sequence from March through January via integer truncation, removing the need for a lookup table.</description>
    /// </item>
    /// <item>
    /// <term><c>100</c>, <c>3</c>, and <c>4</c> (Gregorian Century Correction):</term>
    /// <description>The Gregorian calendar drops leap years on century boundaries (<c>100</c>) unless divisible by <c>400</c>. Subtracting <c>(3 * century) / 4</c> removes exactly 3 leap days every 400 years.</description>
    /// </item>
    /// <item>
    /// <term><c>4800</c> and <c>4900</c> (Positive Offset):</term>
    /// <description>Added to the year to ensure all intermediate values stay strictly positive for ancient BCE dates, avoiding negative integer truncation issues.</description>
    /// </item>
    /// <item>
    /// <term><c>32075</c> (Epoch Alignment):</term>
    /// <description>Subtracts the excess days introduced by the <c>4800</c>/<c>4900</c> year offsets, aligning day <c>0</c> with the start of the Julian Period.</description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="year">Gregorian year as yyyy (e.g., 2026).</param>
    /// <param name="month">Gregorian month of the year as mm (1 for January to 12 for December).</param>
    /// <param name="day">Gregorian day of the month as dd (1 to 31).</param>
    /// <returns>The Julian Day Number corresponding to the given Gregorian date.</returns>
    private static long GregorianDayNumber(long year, long month, long day)
    {
        long leapYearShift = (month - 14) / 12; // MUST truncate towards zero
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