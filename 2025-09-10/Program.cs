// See https://aka.ms/new-console-template for more information
using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Bitte genau zwei Brüche angeben, z.B.: dotnet run \"2 3/8\" \"1 5/6\"");
                return;
            }

            Fraction f1 = ParseMixedFraction(args[0]);
            Fraction f2 = ParseMixedFraction(args[1]);

            Fraction sum = f1 + f2;
            Console.WriteLine(sum.ToMixedString());
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Eingabefehler: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ungültiger Wert: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}");
        }
    }

    static Fraction ParseMixedFraction(string input)
    {
        try
        {
            // Beispiel: "2 3/8" oder "3/4"
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int whole = 0;
            int numerator;
            int denominator;

            if (parts.Length == 2)
            {
                whole = int.Parse(parts[0]);
                string[] frac = parts[1].Split('/');
                if (frac.Length != 2) throw new FormatException($"Ungültiger Bruch: {parts[1]}");
                numerator = int.Parse(frac[0]);
                denominator = int.Parse(frac[1]);
            }
            else if (parts.Length == 1 && parts[0].Contains('/'))
            {
                string[] frac = parts[0].Split('/');
                if (frac.Length != 2) throw new FormatException($"Ungültiger Bruch: {parts[0]}");
                numerator = int.Parse(frac[0]);
                denominator = int.Parse(frac[1]);
            }
            else if (parts.Length == 1)
            {
                whole = int.Parse(parts[0]);
                numerator = 0;
                denominator = 1;
            }
            else
            {
                throw new FormatException($"Ungültiges Eingabeformat: {input}");
            }

            return new Fraction(whole * denominator + numerator, denominator);
        }
        catch (FormatException)
        {
            throw; // direkt weiterwerfen, um im Main gefangen zu werden
        }
        catch (Exception ex)
        {
            throw new FormatException($"Fehler beim Parsen von '{input}': {ex.Message}");
        }
    }
}

class Fraction
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Nenner darf nicht 0 sein.");

        // Kürzen
        int g = Gcd(Math.Abs(numerator), Math.Abs(denominator));
        Numerator = numerator / g;
        Denominator = denominator / g;

        if (Denominator < 0)
        {
            Numerator = -Numerator;
            Denominator = -Denominator;
        }
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int numerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
        int denominator = a.Denominator * b.Denominator;
        return new Fraction(numerator, denominator);
    }

    public string ToMixedString()
    {
        int whole = Numerator / Denominator;
        int remainder = Math.Abs(Numerator % Denominator);

        if (remainder == 0) return whole.ToString();
        if (whole == 0) return $"{remainder}/{Denominator}";
        return $"{whole} {remainder}/{Denominator}";
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }
}
