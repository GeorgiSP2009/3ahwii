using System;

class Bruch
{
    private int ganz;
    private int zaehler;
    private int nenner;

    public Bruch(string bruchtext)
    {
        try
        {
            // Beispiele: "3 4/5" oder "2/3" oder "5"
            if (bruchtext.Contains(' '))
            {
                string[] teile1 = bruchtext.Split(' ');
                this.ganz = int.Parse(teile1[0]);
                string[] teile = teile1[1].Split('/');
                this.zaehler = int.Parse(teile[0]);
                this.nenner = int.Parse(teile[1]);
            }
            else if (bruchtext.Contains('/'))
            {
                this.ganz = 0;
                string[] teile = bruchtext.Split('/');
                this.zaehler = int.Parse(teile[0]);
                this.nenner = int.Parse(teile[1]);
            }
            else
            {
                this.ganz = int.Parse(bruchtext);
                this.zaehler = 0;
                this.nenner = 1;
            }

            if (nenner == 0)
                throw new DivideByZeroException("Nenner darf nicht 0 sein.");
        }
        catch (FormatException)
        {
            Console.WriteLine($"Fehler: '{bruchtext}' ist kein gültiger Bruch. Verwende z. B. '3 1/2' oder '2/3'.");
            this.ganz = 0;
            this.zaehler = 0;
            this.nenner = 1;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Fehler: " + ex.Message);
            this.ganz = 0;
            this.zaehler = 0;
            this.nenner = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unerwarteter Fehler: " + ex.Message);
            this.ganz = 0;
            this.zaehler = 0;
            this.nenner = 1;
        }
    }

    public Bruch(int ganz, int zaehler, int nenner)
    {
        if (nenner == 0) throw new DivideByZeroException("Nenner darf nicht 0 sein.");
        this.ganz = ganz;
        this.zaehler = zaehler;
        this.nenner = nenner;
        Kuerzen();
    }

    private void Kuerzen()
    {
        // Umwandlung in unechten Bruch
        int gesamtZaehler = ganz * nenner + zaehler;
        int g = GGT(Math.Abs(gesamtZaehler), Math.Abs(nenner));
        gesamtZaehler /= g;
        nenner /= g;

        // Zurück in gemischte Form
        ganz = gesamtZaehler / nenner;
        zaehler = Math.Abs(gesamtZaehler % nenner);
    }

    private int GGT(int a, int b)
    {
        while (b != 0)
        {
            int t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public Bruch addiere(Bruch b)
    {
        try
        {
            int z1 = (this.ganz * this.nenner) + this.zaehler;
            int z2 = (b.ganz * b.nenner) + b.zaehler;
            int gemeinsamerNenner = this.nenner * b.nenner;
            int summeZaehler = z1 * b.nenner + z2 * this.nenner;

            Bruch ergebnis = new Bruch(0, summeZaehler, gemeinsamerNenner);
            ergebnis.Kuerzen();
            return ergebnis;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Addieren: " + ex.Message);
            return new Bruch(0, 0, 1);
        }
    }

    public string toString()
    {
        try
        {
            if (nenner == 0) return "Ungültiger Bruch (Nenner = 0)";
            if (zaehler == 0) return $"{ganz}";
            if (ganz == 0) return $"{zaehler}/{nenner}";
            return $"{ganz} {zaehler}/{nenner}";
        }
        catch (Exception ex)
        {
            return "Fehler bei der Ausgabe: " + ex.Message;
        }
    }
}
