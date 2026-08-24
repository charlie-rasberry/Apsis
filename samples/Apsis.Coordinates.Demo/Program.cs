using System.Diagnostics;

using Apsis;
using Apsis.Time;

// currently doesn't account for days
while (true)
{
    Console.WriteLine("INTERACTIVE MODE ? [Y]/n");
    string? input = Console.ReadLine()?.ToLower();
    if (input is "y" or "")
    {
        Console.WriteLine("Enter a date, year / month / day / hour");
        Console.WriteLine("year: ");
        input = Console.ReadLine();
        if (long.TryParse(input, out long y))
        {
            Console.WriteLine("Ok");
        }
        else
        {
            Console.WriteLine("Error");
        }

        Console.WriteLine("month: ");
        input = Console.ReadLine();
        if (long.TryParse(input, out long m))
        {
            Console.WriteLine("Ok");
        }
        else
        {
            Console.WriteLine("Error");
        }

        Console.WriteLine("day: ");
        input = Console.ReadLine();
        if (long.TryParse(input, out long d))
        {
            Console.WriteLine("Ok");
        }
        else
        {
            Console.WriteLine("Error");
        }

        Console.WriteLine("ut1: ");
        input = Console.ReadLine();
        if (double.TryParse(input, out double h))
        {
            Console.WriteLine("Ok");
        }
        else
        {
            Console.WriteLine("Error");
        }
        var epoch = Apsis.Time.Epoch.JulianDayFromGregorian(y,  m, d, h);
        Console.WriteLine($"Epoch: {epoch}");
        

    } else if (input == "n")
    {
        long year = 2192;
        long month = 2;
        long day = 13;
        double ut1 = 4;
        
        var epoch = Epoch.JulianDayFromGregorian(year, month, day, ut1);
        Console.WriteLine($"Setting epoch as {year}-{month}-{day} {ut1} Hours");
        Console.WriteLine($"Current epoch in stored form: {epoch}");
        Console.WriteLine($"Current epoch in display form: {Epoch.StoredJulianDayToDisplayJulianDay(epoch)}");
        
        // TODO: create a tick which uses Stopwatch as realtime, and work its way up to days / weeks passing as seconds etc.
        
        // 12107445442333 - 12104445372131 = 3000070202 nanoseconds
        long start = Stopwatch.GetTimestamp(); // Nanoseconds
        
        // work
        Thread.Sleep(3000);
        
        long end = Stopwatch.GetTimestamp();
        
        Console.WriteLine($"End ({end} - {start}");
        Duration elapsed = new((long) ((double) (end - start) / 1000.0));
        Console.WriteLine($"Elapsed: {elapsed.MicroSeconds} microseconds");
        Console.WriteLine($"Seconds: {elapsed.Seconds} seconds");
        
        Console.WriteLine($"{(double) elapsed.MicroSeconds / Duration.MicrosecondsPerDay:F12} days");
        
        var newEpoch = Epoch.ToJulianDayFromUs(elapsed.MicroSeconds);
        Console.WriteLine($"New Epoch: {newEpoch}");
        
        // updated epoch
        //epoch += newEpoch;
        Console.WriteLine("Updated epoch");
        Console.WriteLine($"Epoch: {epoch}");
        
        Console.WriteLine($"Current epoch in display form: {Epoch.StoredJulianDayToDisplayJulianDay(epoch)}");
    }
    
    
    
}






