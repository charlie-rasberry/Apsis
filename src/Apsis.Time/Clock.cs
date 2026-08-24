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
    

    
}