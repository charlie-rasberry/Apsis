namespace Apsis.Time;


/// <summary>
/// This file does more than whats currently needed.
/// Wont be doing this again as its just confusing to whats being used.
/// 
/// Represents a span of time stored internally as a whole number of microseconds.
/// Provides conversion properties, factory methods, arithmetic operators, and
/// comparison operators for working with durations.
/// </summary>
/// <param name="Us">
/// The total duration expressed in microseconds.
/// </param>
public record struct Duration(long Us)
{
    // Constants

    /// <summary>
    /// The number of microseconds in one millisecond.
    /// </summary>
    public const long MicrosecondsPerMillisecond = 1_000;

    /// <summary>
    /// The number of microseconds in one second.
    /// </summary>
    public const long MicrosecondsPerSecond = 1_000_000;

    /// <summary>
    /// The number of microseconds in one minute.
    /// </summary>
    public const long MicrosecondsPerMinute = MicrosecondsPerSecond * 60;

    /// <summary>
    /// The number of microseconds in one hour.
    /// </summary>
    public const long MicrosecondsPerHour = MicrosecondsPerMinute * 60;

    /// <summary>
    /// The number of microseconds in one day.
    /// </summary>
    public const long MicrosecondsPerDay = MicrosecondsPerHour * 24;

    /// <summary>
    /// The number of microseconds in one week.
    /// </summary>
    public const long MicrosecondsPerWeek = MicrosecondsPerDay * 7;

    // Properties

    /// <summary>
    /// Gets the total duration in microseconds.
    /// </summary>
    public long MicroSeconds => Us;

    /// <summary>
    /// Gets the total duration in milliseconds.
    /// </summary>
    public long MilliSeconds => Us / MicrosecondsPerMillisecond;

    /// <summary>
    /// Gets the total duration in seconds.
    /// </summary>
    public long Seconds => Us / MicrosecondsPerSecond;

    /// <summary>
    /// Gets the total duration in minutes.
    /// </summary>
    public long Minutes => Us / MicrosecondsPerMinute;

    /// <summary>
    /// Gets the total duration in hours.
    /// </summary>
    public long Hours => Us / MicrosecondsPerHour;

    /// <summary>
    /// Gets the total duration in days.
    /// </summary>
    public long Days => Us / MicrosecondsPerDay;

    /// <summary>
    /// Gets the total duration in weeks.
    /// </summary>
    public long Weeks => Us / MicrosecondsPerWeek;

    // Factories

    /// <summary>
    /// Creates a duration from a microsecond value.
    /// </summary>
    /// <param name="microseconds">The number of microseconds.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromMicroseconds(long microseconds) => new(microseconds);

    /// <summary>
    /// Creates a duration from a millisecond value.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromMilliseconds(long milliseconds) => new(milliseconds * MicrosecondsPerMillisecond);

    /// <summary>
    /// Creates a duration from a second value.
    /// </summary>
    /// <param name="seconds">The number of seconds.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromSeconds(double seconds) => new((long) (seconds * (double) MicrosecondsPerSecond));

    /// <summary>
    /// Creates a duration from a minute value.
    /// </summary>
    /// <param name="minutes">The number of minutes.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromMinutes(long minutes) => new(minutes * MicrosecondsPerMinute);

    /// <summary>
    /// Creates a duration from an hour value.
    /// </summary>
    /// <param name="hours">The number of hours.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromHours(long hours) => new(hours * MicrosecondsPerHour);

    /// <summary>
    /// Creates a duration from a day value.
    /// </summary>
    /// <param name="days">The number of days.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromDays(long days) => new(days * MicrosecondsPerDay);

    /// <summary>
    /// Creates a duration from a week value.
    /// </summary>
    /// <param name="weeks">The number of weeks.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromWeeks(long weeks) => new(weeks * MicrosecondsPerWeek);

    // Getters

    /// <summary>
    /// Gets the total duration in milliseconds.
    /// </summary>
    /// <returns>The total number of milliseconds.</returns>
    public long GetMilliseconds() => MilliSeconds;

    /// <summary>
    /// Gets the total duration in seconds.
    /// </summary>
    /// <returns>The total number of seconds.</returns>
    public long GetSeconds() => Seconds;

    /// <summary>
    /// Gets the total duration in minutes.
    /// </summary>
    /// <returns>The total number of minutes.</returns>
    public long GetMinutes() => Minutes;

    /// <summary>
    /// Gets the total duration in hours.
    /// </summary>
    /// <returns>The total number of hours.</returns>
    public long GetHours() => Hours;

    /// <summary>
    /// Gets the total duration in days.
    /// </summary>
    /// <returns>The total number of days.</returns>
    public long GetDays() => Days;

    /// <summary>
    /// Gets the total duration in weeks.
    /// </summary>
    /// <returns>The total number of weeks.</returns>
    public long GetWeeks() => Weeks;

    // Overloads

    /// <summary>
    /// Adds two durations.
    /// </summary>
    public static Duration operator +(Duration a, Duration b) => new(a.Us + b.Us);

    /// <summary>
    /// Subtracts one duration from another.
    /// </summary>
    public static Duration operator -(Duration a, Duration b) => new(a.Us - b.Us);

    /// <summary>
    /// Multiplies a duration by a scalar.
    /// </summary>
    public static Duration operator *(Duration a, long scalar) => new(a.Us * scalar);

    /// <summary>
    /// Divides a duration by a scalar.
    /// </summary>
    public static Duration operator /(Duration a, long scalar) => new(a.Us / scalar);

    /// <summary>
    /// Calculates the ratio between two durations.
    /// </summary>
    public static double operator /(Duration a, Duration b) => (double)a.Us / b.Us;

    // Comparisons

    /// <summary>
    /// Determines whether one duration is less than another.
    /// </summary>
    public static bool operator <(Duration a, Duration b) => a.Us < b.Us;

    /// <summary>
    /// Determines whether one duration is greater than another.
    /// </summary>
    public static bool operator >(Duration a, Duration b) => a.Us > b.Us;

    /// <summary>
    /// Determines whether one duration is less than or equal to another.
    /// </summary>
    public static bool operator <=(Duration a, Duration b) => a.Us <= b.Us;

    /// <summary>
    /// Determines whether one duration is greater than or equal to another.
    /// </summary>
    public static bool operator >=(Duration a, Duration b) => a.Us >= b.Us;

    /// <summary>
    /// Deconstructs the duration into its microsecond and millisecond representations.
    /// </summary>
    /// <param name="us">The total number of microseconds.</param>
    /// <param name="ms">The total number of milliseconds.</param>
    public void Deconstruct(out long us, out long ms)
    {
        us = Us;
        ms = MilliSeconds;
    }

    /// <summary>
    /// Returns a string representation of the duration in seconds with
    /// six digits of microsecond precision.
    /// </summary>
    /// <returns>A formatted duration string.</returns>
    public override string ToString() => $"{Seconds}.{Us % MicrosecondsPerSecond:D6}s";
}