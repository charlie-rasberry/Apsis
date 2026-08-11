using Apsis;
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
        double epoch = Apsis.Time.Epoch.JulianDayFromGregorian(y,  m, d, h);
        Console.WriteLine($"Epoch: {epoch}");
        

    } else if (input == "n")
    {
        long k = 2192;
        long m = 2;
        long i = 13;
        double ut1 = 4;
        
        double epoch = Apsis.Time.Epoch.JulianDayFromGregorian(k, m, i, ut1);
        Console.WriteLine($"Epoch: {epoch}");
    }
}






