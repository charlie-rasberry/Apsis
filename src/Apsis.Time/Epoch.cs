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
/// <param name="jdHigh"></param>
/// <param name="jdLow"></param>
public record struct Epoch(long jdHigh, long jdLow)
{
}