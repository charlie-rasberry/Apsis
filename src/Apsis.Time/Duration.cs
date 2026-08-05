namespace Apsis.Time;


public readonly record struct Duration(long Us)
{
    // Constants
    public const long MicrosecondsPerMillisecond = 1_000;
    public const long MicrosecondsPerSecond = 1_000_000;
    public const long MicrosecondsPerMinute = MicrosecondsPerSecond * 60;
    public const long MicrosecondsPerHour = MicrosecondsPerMinute * 60;
    public const long MicrosecondsPerDay = MicrosecondsPerHour * 24;
    public const long MicrosecondsPerWeek = MicrosecondsPerDay * 7;

    // Properties
    public long MicroSeconds => Us;
    public long MilliSeconds => Us / MicrosecondsPerMillisecond;
    public long Seconds => Us / MicrosecondsPerSecond;
    public long Minutes => Us / MicrosecondsPerMinute;
    public long Hours => Us / MicrosecondsPerHour;
    public long Days => Us / MicrosecondsPerDay;
    public long Weeks => Us / MicrosecondsPerWeek;
    
    // Factories
    public static Duration FromMicroseconds(long microseconds) => new(microseconds);
    public static Duration FromMilliseconds(long milliseconds) => new(milliseconds * MicrosecondsPerMillisecond);
    public static Duration FromSeconds(long seconds) => new(seconds * MicrosecondsPerSecond);
    public static Duration FromMinutes(long minutes) => new(minutes * MicrosecondsPerMinute);
    public static Duration FromHours(long hours) => new(hours * MicrosecondsPerHour);
    public static Duration FromDays(long days) => new(days * MicrosecondsPerDay);
    public static Duration FromWeeks(long weeks) => new(weeks * MicrosecondsPerWeek);

    
    // Getters
    public long GetMilliseconds() => MilliSeconds;
    public long GetSeconds() => Seconds;
    public long GetMinutes() => Minutes;
    public long GetHours()  => Hours;
    public long GetDays()  => Days;
    public long GetWeeks()  => Weeks;
    
    // Overloads
    public static Duration operator +(Duration a, Duration b) => new (a.Us + b.Us);
    public static Duration operator -(Duration a, Duration b) => new (a.Us - b.Us);
    public static Duration operator *(Duration a, long scalar) => new(a.Us * scalar);
    public static Duration operator /(Duration a, long scalar) => new(a.Us / scalar);
    public static double operator /(Duration a, Duration b) => (double) a.Us / b.Us;


        // Comparisons
    public static bool operator <(Duration a, Duration b) => a.Us < b.Us;
    public static bool operator >(Duration a, Duration b) => a.Us > b.Us;
    public static bool operator <=(Duration a, Duration b) => a.Us <= b.Us;
    public static bool operator >=(Duration a, Duration b) => a.Us >= b.Us;
    

    public void Deconstruct(out long us, out long ms)
    {
        us = Us;
        ms = MilliSeconds;
    }
    
    // Override ToString for better debugging
    public override string ToString() => $"{Seconds}.{Us % MicrosecondsPerSecond:D6}s";
    
}