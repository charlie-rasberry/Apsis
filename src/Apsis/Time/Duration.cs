using System.Diagnostics;

namespace Apsis.Time;


/// <summary>
/// Represents a span of time stored internally as a whole number of microseconds.
/// Provides conversion properties, factory methods, arithmetic operators, and
/// comparison operators for working with durations.
/// </summary>
/// <param name="Us">
/// The total duration expressed in microseconds.
/// </param>
public readonly record struct Duration(long Us) : IComparable<Duration>
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
    public long Microseconds => Us;

    /// <summary>
    /// Gets the total duration in milliseconds.
    /// </summary>
    public long Milliseconds => Us / MicrosecondsPerMillisecond;

    /// <summary>
    /// Gets the total duration in seconds.
    /// </summary>
    public long Seconds => Us / MicrosecondsPerSecond;
    
    /// <summary>
    /// Gets the total <see cref="Duration"/> value in whole and fractional seconds.
    /// </summary>
    public double TotalSeconds => (double) Us / MicrosecondsPerSecond;

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

    /// <summary>
    /// Alias for an empty or default <see cref="Duration"/>.
    /// </summary>
    public static Duration Zero => default;

    // Factories

    /// <summary>
    /// Creates a duration from a microsecond value.
    /// </summary>
    /// <param name="microseconds">The number of microseconds.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromMicroseconds(long microseconds) => new(microseconds); // Debating whether to delete the constructor or this instead

    /// <summary>
    /// Creates a duration from a millisecond value.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds.</param>
    /// <returns>A <see cref="Duration"/> representing the specified value.</returns>
    public static Duration FromMilliseconds(long milliseconds) => new(milliseconds * MicrosecondsPerMillisecond);
    
    /// <summary>
    /// Creates a duration instance from a second value.
    /// </summary>
    /// <param name="seconds">Number of seconds.</param>
    /// <returns>A new <see cref="Duration"/> instance.</returns>
    public static Duration FromSeconds(long seconds) => new(seconds * MicrosecondsPerSecond);
    
    /// <summary>
    /// Creates a duration from stopwatch ticks
    /// </summary>
    /// <param name="ticks">The number of ticks.</param>
    /// <returns>A duration representing the specified value.</returns>
    public static Duration FromStopwatchTick(long ticks) 
    {
        long microseconds = (ticks / Stopwatch.Frequency) * 1_000_000 + (ticks % Stopwatch.Frequency) * 1_000_000 / Stopwatch.Frequency; // convert tick nanoseconds to microseconds, 1_000_000 to convert seconds to microseconds
        return new Duration(microseconds); 
    }
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

    
    /// <summary>
    /// Returns -1 for less than, 0 for equal to, 1 for greater than.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Duration other) => Us.CompareTo(other.Us);
    
    // Overloads

    /// <summary>
    /// Adds two <see cref="Duration"/> values.
    /// </summary>
    public static Duration operator +(Duration a, Duration b) => new(a.Us + b.Us);

    /// <summary>
    /// Subtracts one <see cref="Duration"/> value from another.
    /// </summary>
    public static Duration operator -(Duration a, Duration b) => new(a.Us - b.Us);
    
    /// <summary>
    /// Negates a <see cref="Duration"/>
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static Duration operator -(Duration duration) => new(-duration.Us);

    /// <summary>
    /// Multiplies a <see cref="Duration"/> value by a scalar.
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
        ms = Milliseconds;
    }

    /// <summary>
    /// Returns a string representation of the duration in seconds with
    /// six digits of microsecond precision.
    /// </summary>
    /// <returns>A formatted duration string.</returns>
    public override string ToString()
    {
        return $"{(Microseconds >= 0 ? "" : "-")}{Math.Abs(Seconds)}.{Math.Abs(Us % MicrosecondsPerSecond):D6}s";
    }
}