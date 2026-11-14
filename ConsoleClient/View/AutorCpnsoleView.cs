using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class AutorConsoleView
{
    private readonly AutorServis _autorServis;
    private readonly AdresaServis _adresaServis;

    public AutorConsoleView(AutorServis autorServis, AdresaServis adresaServis)
    {
        _autorServis = autorServis;
        _adresaServis = adresaServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- AUTORI ---");
            Console.WriteLine("1: Prikazi sve autore");
            Console.WriteLine("2: Dodaj autora");
            Console.WriteLine("3: Izmeni autora");
            Console.WriteLine("4: Obrisi autora");
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
        List<Autor> autori = _autorServis.DobaviSve();
        if (autori.Count == 0)
        {
            Console.WriteLine("Nema autora.");
            return;
        }

        foreach (Autor autor in autori)
        {
            Console.WriteLine(autor);
        }
    }

    private void Dodaj()
    {
        Autor autor = UnesiAutora();
        _autorServis.Dodaj(autor);
        Console.WriteLine("Autor je dodat.");
    }

    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id autora: ");
        if (_autorServis.PronadjiPoId(id) == null)
        {
            Console.WriteLine("Autor nije pronadjen.");
            return;
        }

        Autor autor = UnesiAutora();
        autor.Id = id;
        _autorServis.Izmeni(autor);
        Console.WriteLine("Autor je izmenjen.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id autora: ");
        Autor? obrisan = _autorServis.Obrisi(id);
        Console.WriteLine(obrisan == null ? "Autor nije pronadjen." : "Autor je obrisan.");
    }

    private Autor UnesiAutora()
    {
        string ime = Unos.Tekst("Ime: ");
        string prezime = Unos.Tekst("Prezime: ");
        DateTime datumRodjenja = Unos.Datum("Datum rodjenja (dd.MM.yyyy): ");

        foreach (Adresa adresa in _adresaServis.DobaviSve())
        {
            Console.WriteLine($"[{adresa.Id}] {adresa}");
        }

        int adresaId = Unos.CeoBroj("Id adrese: ");
        string telefon = Unos.Tekst("Telefon: ");
        string email = Unos.Tekst("Email: ");
        string brojLicneKarte = Unos.Tekst("Broj licne karte: ");
        int godineIskustva = Unos.CeoBroj("Godine iskustva: ", 0, 100);

        return new Autor(ime, prezime, datumRodjenja, adresaId, telefon, email, brojLicneKarte, godineIskustva);
    }
}