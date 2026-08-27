using System.Diagnostics;

using Apsis;
using Apsis.Coordinates;
using Apsis.Time;

// currently doesn't account for days
     /**
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
        */
        //      London, UK
        //      Latitude and longitude coordinates are: 51.509865, -0.118092
        // on the first run we got                                      X: 2026736474463283 Y: 1933185610758203  Z: 5988631442304806
        // on the second run we got                                     X: 3961488367666185 Y: 3100724252326595  Z: 4948692787864965
        // on the third run with a new prime vertical formula we got:   X: 3977787910007 Y: 3113482181051  Z: 4969054239987
        // 
        var LlaToEcefTest = EcefCoordinate.FromGeodeticLla(51.509865, -0.118092, 11);
        Console.WriteLine($"In micrometers: X: {LlaToEcefTest.EcefXum} Y: {LlaToEcefTest.EcefYum}  Z: {LlaToEcefTest.EcefZum}");
        Console.WriteLine($"In meters: X: {LlaToEcefTest.EcefX} Y: {LlaToEcefTest.EcefY}  Z: {LlaToEcefTest.EcefZ}");






