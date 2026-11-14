using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class AdresaConsoleView
{
    private readonly AdresaServis _adresaServis;

    public AdresaConsoleView(AdresaServis adresaServis)
    {
        _adresaServis = adresaServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- ADRESE ---");
            Console.WriteLine("1: Prikazi sve adrese");
            Console.WriteLine("2: Dodaj adresu");
            Console.WriteLine("3: Izmeni adresu");
            Console.WriteLine("4: Obrisi adresu");
            Console.WriteLine("0: Nazad");

            string izbor = Console.ReadLine() ?? "0";
            if (izbor == "0") return;

            try
            {
                switch (izbor)
                {
                    case "1": PrikaziSve(); break;
                    case "2": Dodaj(); break;
                    case "3": Izmeni(); break;
                    case "4": Obrisi(); break;
                    default: Console.WriteLine("Nepostojeca opcija."); break;
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Greska: {e.Message}");
            }
        }
    }

    private void PrikaziSve()
    {
        List<Adresa> adrese = _adresaServis.DobaviSve();
        if (adrese.Count == 0)
        {
            Console.WriteLine("Nema adresa.");
            return;
        }

        foreach (Adresa adresa in adrese)
        {
            Console.WriteLine($"[{adresa.Id}] {adresa}");
        }
    }

    private void Dodaj()
    {
        Adresa adresa = UnesiAdresu();
        _adresaServis.Dodaj(adresa);
        Console.WriteLine("Adresa je dodata.");
    }
    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id adrese: ");
        if (_adresaServis.PronadjiPoId(id) == null)
        {
            Console.WriteLine("Adresa nije pronadjena.");
            return;
        }

        Adresa adresa = UnesiAdresu();
        adresa.Id = id;
        _adresaServis.Izmeni(adresa);
        Console.WriteLine("Adresa je izmenjena.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id adrese: ");
        Adresa? obrisana = _adresaServis.Obrisi(id);
        Console.WriteLine(obrisana == null ? "Adresa nije pronadjena." : "Adresa je obrisana.");
    }

    private Adresa UnesiAdresu()
    {
        string ulica = Unos.Tekst("Ulica: ");
        string broj = Unos.Tekst("Broj: ");
        string grad = Unos.Tekst("Grad: ");
        string drzava = Unos.Tekst("Drzava: ");

        return new Adresa(ulica, broj, grad, drzava);
    }
}