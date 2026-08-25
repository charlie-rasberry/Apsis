using System.Diagnostics;

using Apsis;
using Apsis.Time;

// currently doesn't account for days
while (true)
{
        long year = 2192;
        long month = 2;
        long day = 13;
        double ut1 = 4;
        
        Epoch epoch = Epoch.JulianDayFromGregorian(year, month, day, ut1);
        Console.WriteLine($"Setting epoch as {year}-{month}-{day} {ut1} Hours");
        Console.WriteLine($"Current epoch in stored form: {epoch.JulianMicroseconds}");
        Console.WriteLine($"Current epoch in display form: {epoch.JulianDay}");
        
        long start = Stopwatch.GetTimestamp(); // Nanoseconds
        
        // work
        Thread.Sleep(3000);
        
        long end = Stopwatch.GetTimestamp();
        
        Console.WriteLine($"End ({end} - {start}");
        Duration elapsed = new((long) ((double) (end - start) / 1000.0));
        Console.WriteLine($"Elapsed: {elapsed.Microseconds} microseconds");
        Console.WriteLine($"Seconds: {elapsed.Seconds} seconds");
        
        Console.WriteLine($"{(double) elapsed.Microseconds / Duration.MicrosecondsPerDay:F12} days");
        
        var newEpoch = Epoch.ToJulianDayFromUs(elapsed.Microseconds);
        Console.WriteLine($"New Epoch: {newEpoch.JulianMicroseconds} Julian Microseconds, {newEpoch.JulianDay} Julian Days");
        

        Console.WriteLine($"Epoch + new epoch: {newEpoch.JulianDay} + {epoch.JulianDay} =  {newEpoch.JulianDay + epoch.JulianDay} Julian Days, {newEpoch.JulianMicroseconds + epoch.JulianMicroseconds}");
        var tmpJulianMicroseconds = epoch + elapsed;
        Console.WriteLine($"new Julian Microseconds: {tmpJulianMicroseconds}");
        Console.WriteLine($"Converted from the stored + elapsed: {tmpJulianMicroseconds.JulianMicroseconds} microseconds,  {tmpJulianMicroseconds.JulianDay} Julian Days");
}






