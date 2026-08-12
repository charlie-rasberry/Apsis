namespace Apsis.Time;
// TODO: Current implementation is a day off when the date goes past a certain threshold.
// TODO: input 12th march and it might become 13th march

/// <summary>
/// Provides utility methods for converting between civil calendar dates and Julian dates,
/// including determining whether a given date belongs to the Julian or Gregorian calendar.
/// </summary>

// JD = 367K - <(7(K+<(M+9)/12>))/4> + <(275M)/9> + I + 1721013.5 + UT1/24
// - 0.5sign(100K+M-190002.5) + 0.5

// where K is the year (1801 <= K <= 2099), M is the month
// (1 <= M <= 12), I is the day of the month (1 <= I <= 31),
// and UT is the universal time in hours ("<=" means "less than or equal to").

public static class Clock
{
    /// <summary>
    /// Converts a Gregorian or Julian calendar date into its corresponding Julian Date
    /// represented as separate integer and fractional components.
    /// </summary>
    /// <param name="K">
    /// The calendar year. Valid range for this implementation is typically 1801 to 2099.
    /// </param>
    /// <param name="M">
    /// The calendar month, where 1 is January and 12 is December.
    /// </param>
    /// <param name="I">
    /// The day of the month.
    /// </param>
    /// <param name="ut1">
    /// The Universal Time (UT1) expressed as whole hours.
    /// </param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// The integer Julian Date component.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The fractional Julian Date component.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>

    // K is the year (1801 <= K <= 2099),
    // M is the month (1 <= M <= 12),
    // I is the day of the month (1 <= I <= 31),
    // B is Julian calendar default
    // and UT is the universal time in hours
    public static (long, long) GregorianToJulian(long K, long M, long I, double ut1)
    {
        long B = 0; // Julian calendar default

        // For Julian Date algorithms, think of the calendar year as starting on 1 March and ending on 29 February
        //if (M == 1 || M == 2)
        //{
        //    K -= 1;
        //    M += 12;
        //}

        if (!IsJulianDate(K, M, I)) // convert to Gregorian calendar
        {
            long A = K / 100;
            B = 2 - A + (A / 4);
        }

        long C = ((100 * K) + M < 190002.5) ? 1 : 0;

        long N = (367 * K)
                - (7 * (K + (M + 9) / 12)) / 4
                + (275 * M) / 9
                + I
                + C
                + 1721013;

        long jdInt = N + (ut1 >= 12 ? 1 : 0);
        
        long jdFrac = (long)((((ut1 + 12.0) % 24.0) / 24.0) * 10000000);

        return (jdInt, jdFrac);
        // JDN = N + 1
    }

    /// <summary>
    /// Determines whether the supplied calendar date falls within the Julian calendar
    /// rather than the Gregorian calendar.
    /// </summary>
    /// <param name="year">
    /// The calendar year.
    /// </param>
    /// <param name="month">
    /// The calendar month.
    /// </param>
    /// <param name="day">
    /// The day of the month.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the date is in the Julian calendar;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsJulianDate(long year, long month, long day)
    {
        if (year > 1582)
            return false;
        if (year < 1582)
            return true;
        // year is 1582 so check month
        if (month > 10)
            return false;
        if (month < 10)
            return true;
        // month is 10 so check days
        if (day > 14)
            return false;
        return true;
    }
}