using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class IzdavacConsoleView
{
    private readonly IzdavacServis _izdavacServis;
    private readonly AutorServis _autorServis;

    public IzdavacConsoleView(IzdavacServis izdavacServis, AutorServis autorServis)
    {
        _izdavacServis = izdavacServis;
        _autorServis = autorServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- IZDAVACI ---");
            Console.WriteLine("1: Prikazi sve izdavace");
            Console.WriteLine("2: Dodaj izdavaca");
            Console.WriteLine("3: Izmeni izdavaca");
            Console.WriteLine("4: Obrisi izdavaca");
            Console.WriteLine("5: Dodaj autora izdavacu");
            Console.WriteLine("6: Ukloni autora sa izdavaca");
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
                    case "5": DodajAutora(); break;
                    case "6": UkloniAutora(); break;
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
        List<Izdavac> izdavaci = _izdavacServis.DobaviSve();
        if (izdavaci.Count == 0)
        {
            Console.WriteLine("Nema izdavaca.");
            return;
        }

        foreach (Izdavac izdavac in izdavaci)
        {
            Console.WriteLine($"[{izdavac.Id}] {izdavac}");
        }
    }

    private void Dodaj()
    {
        Izdavac izdavac = UnesiIzdavaca();
        _izdavacServis.Dodaj(izdavac);
        Console.WriteLine("Izdavac je dodat.");
    }

    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id izdavaca: ");
        if (_izdavacServis.PronadjiPoId(id) == null)
        {
            Console.WriteLine("Izdavac nije pronadjen.");
            return;
        }

        Izdavac izdavac = UnesiIzdavaca();
        izdavac.Id = id;
        _izdavacServis.Izmeni(izdavac);
        Console.WriteLine("Izdavac je izmenjen.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id izdavaca: ");
        Izdavac? obrisan = _izdavacServis.Obrisi(id);
        Console.WriteLine(obrisan == null ? "Izdavac nije pronadjen." : "Izdavac je obrisan.");
    }

    private void DodajAutora()
    {
        int izdavacId = Unos.CeoBroj("Id izdavaca: ");
        PrikaziAutore();
        int autorId = Unos.CeoBroj("Id autora: ");
        _izdavacServis.DodajAutora(izdavacId, autorId);
        Console.WriteLine("Autor je dodat izdavacu.");
    }

    private void UkloniAutora()
    {
        int izdavacId = Unos.CeoBroj("Id izdavaca: ");
        int autorId = Unos.CeoBroj("Id autora: ");
        _izdavacServis.UkloniAutora(izdavacId, autorId);
        Console.WriteLine("Autor je uklonjen sa izdavaca.");
    }

    private void PrikaziAutore()
    {
        foreach (Autor autor in _autorServis.DobaviSve())
        {
            Console.WriteLine($"[{autor.Id}] {autor.Ime} {autor.Prezime} ({autor.GodineIskustva} god. iskustva)");
        }
    }

    private Izdavac UnesiIzdavaca()
    {
        string sifra = Unos.Tekst("Sifra: ");
        string naziv = Unos.Tekst("Naziv: ");
        PrikaziAutore();
        int? sefId = Unos.OpcioniCeoBroj("Id sefa (Enter ako nema): ");

        return new Izdavac(sifra, naziv, sefId);
    }
}
