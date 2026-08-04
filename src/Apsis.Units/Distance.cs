namespace Apsis.Units;

public readonly record struct Distance(double Meters)
{
    public double Kilometers => Meters / 1000;
    public static Distance FromKilometers(double km) => new Distance(km * 1000);
    
    public static Distance operator +(Distance a, Distance b) => new Distance(a.Meters + b.Meters);
    public static Distance operator -(Distance a, Distance b) => new Distance(a.Meters - b.Meters);
}