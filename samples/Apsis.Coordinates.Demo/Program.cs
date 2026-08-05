Console.WriteLine("Hello, World!");
Console.WriteLine("Enter a date, year / month / day / hour");

Console.WriteLine("year: ");
string? year = Console.ReadLine();
if (long.TryParse(year, out long k))
{
    Console.WriteLine("Ok");
}
else
{
    Console.WriteLine("Error");
}

Console.WriteLine("month: ");
string? month = Console.ReadLine();
if (long.TryParse(month, out long m))
{
    Console.WriteLine("Ok");
}
else
{
    Console.WriteLine("Error");
}

Console.WriteLine("day: ");
string? day = Console.ReadLine();
if (long.TryParse(day, out long i))
{
    Console.WriteLine("Ok");
}
else
{
    Console.WriteLine("Error");
}

Console.WriteLine("ut1: ");
string? time = Console.ReadLine();
if (long.TryParse(time, out long ut1))
{
    Console.WriteLine("Ok");
}
else
{
    Console.WriteLine("Error");
}

(long, long) J = Apsis.Time.Clock.GregorianToJulian(k, m, i, ut1);
Console.WriteLine($"Julian day: {J.Item1}{J.Item2}");



