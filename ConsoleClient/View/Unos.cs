using System.Globalization;

namespace ConsoleClient.View;

// Preuzeto sa vezbi (CRUDExample, ConsoleViewUtils)
public static class Unos
{
    public static string Tekst(string poruka)
    {
        while (true)
        {
            Console.Write(poruka);
            string unos = (Console.ReadLine() ?? string.Empty).Trim();

            if (unos.Length == 0)
                Console.WriteLine("Polje ne sme biti prazno.");
            else if (unos.Contains('|'))
                Console.WriteLine("Znak '|' nije dozvoljen.");
            else
                return unos;
        }
    }

    public static int CeoBroj(string poruka, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(poruka);
            string unos = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(unos, out int broj) && broj >= min && broj <= max)
                return broj;

            Console.WriteLine("Neispravan broj, pokusajte ponovo.");
        }
    }

    public static int? OpcioniCeoBroj(string poruka)
    {
        while (true)
        {
            Console.Write(poruka);
            string unos = (Console.ReadLine() ?? string.Empty).Trim();

            if (unos.Length == 0)
                return null;
            if (int.TryParse(unos, out int broj))
                return broj;

            Console.WriteLine("Neispravan broj, pokusajte ponovo.");
        }
    }

    public static decimal DecimalniBroj(string poruka)
    {
        while (true)
        {
            Console.Write(poruka);
            string unos = (Console.ReadLine() ?? string.Empty).Replace(',', '.');

            if (decimal.TryParse(unos, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal broj) && broj >= 0)
                return broj;

            Console.WriteLine("Neispravan iznos, pokusajte ponovo.");
        }
    }
    public static DateTime Datum(string poruka)
    {
        while (true)
        {
            Console.Write(poruka);
            string unos = Console.ReadLine() ?? string.Empty;

            if (DateTime.TryParseExact(unos, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime datum))
                return datum;

            Console.WriteLine("Datum mora biti u formatu dd.MM.yyyy.");
        }
    }
    public static List<int> ListaBrojeva(string poruka)
    {
        while (true)
        {
            Console.Write(poruka);
            string[] delovi = (Console.ReadLine() ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            List<int> brojevi = new List<int>();
            foreach (string deo in delovi)
            {
                if (int.TryParse(deo, out int broj))
                    brojevi.Add(broj);
            }

            if (brojevi.Count > 0 && brojevi.Count == delovi.Length)
                return brojevi;

            Console.WriteLine("Unesite brojeve odvojene zarezom, npr. 0,2");
        }
    }
}
