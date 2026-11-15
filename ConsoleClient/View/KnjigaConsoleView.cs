using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class KnjigaConsoleView
{
    private readonly KnjigaServis _knjigaServis;
    private readonly AutorServis _autorServis;
    private readonly IzdavacServis _izdavacServis;

    public KnjigaConsoleView(KnjigaServis knjigaServis, AutorServis autorServis, IzdavacServis izdavacServis)
    {
        _knjigaServis = knjigaServis;
        _autorServis = autorServis;
        _izdavacServis = izdavacServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- KNJIGE ---");
            Console.WriteLine("1: Prikazi sve knjige");
            Console.WriteLine("2: Dodaj knjigu");
            Console.WriteLine("3: Izmeni knjigu");
            Console.WriteLine("4: Obrisi knjigu");
            Console.WriteLine("5: Dodaj autora knjizi");
            Console.WriteLine("6: Ukloni autora sa knjige");
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
        List<Knjiga> knjige = _knjigaServis.DobaviSve();
        if (knjige.Count == 0)
        {
            Console.WriteLine("Nema knjiga.");
            return;
        }

        foreach (Knjiga knjiga in knjige)
        {
            Console.WriteLine($"[{knjiga.Id}] {knjiga}");
        }
    }

    private void Dodaj()
    {
        Knjiga knjiga = UnesiKnjigu();
        PrikaziAutore();
        List<int> autorIds = Unos.ListaBrojeva("Id-jevi autora (odvojeni zarezom): ");
        _knjigaServis.Dodaj(knjiga, autorIds);
        Console.WriteLine("Knjiga je dodata.");
    }

    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id knjige: ");
        if (_knjigaServis.PronadjiPoId(id) == null)
        {
            Console.WriteLine("Knjiga nije pronadjena.");
            return;
        }

        Knjiga knjiga = UnesiKnjigu();
        knjiga.Id = id;
        _knjigaServis.Izmeni(knjiga);
        Console.WriteLine("Knjiga je izmenjena.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id knjige: ");
        Knjiga? obrisana = _knjigaServis.Obrisi(id);
        Console.WriteLine(obrisana == null ? "Knjiga nije pronadjena." : "Knjiga je obrisana.");
    }

    private void DodajAutora()
    {
        int knjigaId = Unos.CeoBroj("Id knjige: ");
        PrikaziAutore();
        int autorId = Unos.CeoBroj("Id autora: ");
        _knjigaServis.DodajAutora(knjigaId, autorId);
        Console.WriteLine("Autor je dodat knjizi.");
    }

    private void UkloniAutora()
    {
        int knjigaId = Unos.CeoBroj("Id knjige: ");
        int autorId = Unos.CeoBroj("Id autora: ");
        _knjigaServis.UkloniAutora(knjigaId, autorId);
        Console.WriteLine("Autor je uklonjen sa knjige.");
    }

    private void PrikaziAutore()
    {
        foreach (Autor autor in _autorServis.DobaviSve())
        {
            Console.WriteLine($"[{autor.Id}] {autor.Ime} {autor.Prezime}");
        }
    }

    private Knjiga UnesiKnjigu()
    {
        string isbn = Unos.Tekst("ISBN: ");
        string naziv = Unos.Tekst("Naziv: ");
        string zanr = Unos.Tekst("Zanr: ");
        int godinaIzdanja = Unos.CeoBroj("Godina izdanja: ", 0, 2100);
        decimal cena = Unos.DecimalniBroj("Cena: ");
        int brojStrana = Unos.CeoBroj("Broj strana: ", 1, 100000);

        foreach (Izdavac izdavac in _izdavacServis.DobaviSve())
        {
            Console.WriteLine($"[{izdavac.Id}] {izdavac.Naziv}");
        }

        int? izdavacId = Unos.OpcioniCeoBroj("Id izdavaca (Enter ako nema): ");

        return new Knjiga(isbn, naziv, zanr, godinaIzdanja, cena, brojStrana, izdavacId);
    }
}