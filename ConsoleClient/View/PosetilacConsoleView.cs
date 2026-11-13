using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class PosetilacConsoleView
{
    private readonly PosetilacServis _posetilacServis;
    private readonly AdresaServis _adresaServis;
    private readonly KnjigaServis _knjigaServis;

    public PosetilacConsoleView(PosetilacServis posetilacServis, AdresaServis adresaServis, KnjigaServis knjigaServis)
    {
        _posetilacServis = posetilacServis;
        _adresaServis = adresaServis;
        _knjigaServis = knjigaServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- POSETIOCI ---");
            Console.WriteLine("1: Prikazi sve posetioce");
            Console.WriteLine("2: Dodaj posetioca");
            Console.WriteLine("3: Izmeni posetioca");
            Console.WriteLine("4: Obrisi posetioca");
            Console.WriteLine("5: Prikazi listu zelja");
            Console.WriteLine("6: Dodaj knjigu na listu zelja");
            Console.WriteLine("7: Ukloni knjigu sa liste zelja");
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
                    case "5": PrikaziListuZelja(); break;
                    case "6": DodajNaListuZelja(); break;
                    case "7": UkloniSaListeZelja(); break;
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
        List<Posetilac> posetioci = _posetilacServis.DobaviSve();
        if (posetioci.Count == 0)
        {
            Console.WriteLine("Nema posetilaca.");
            return;
        }

        foreach (Posetilac posetilac in posetioci)
        {
            Console.WriteLine($"[{posetilac.Id}] {posetilac}");
        }
    }

    private void Dodaj()
    {
        Posetilac posetilac = UnesiPosetioca();
        _posetilacServis.Dodaj(posetilac);
        Console.WriteLine("Posetilac je dodat.");
    }

    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id posetioca: ");
        Posetilac? postojeci = _posetilacServis.PronadjiPoId(id);
        if (postojeci == null)
        {
            Console.WriteLine("Posetilac nije pronadjen.");
            return;
        }

        Console.WriteLine($"Trenutno: {postojeci}");
        Posetilac posetilac = UnesiPosetioca();
        posetilac.Id = id;
        _posetilacServis.Izmeni(posetilac);
        Console.WriteLine("Posetilac je izmenjen.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id posetioca: ");
        Posetilac? obrisan = _posetilacServis.Obrisi(id);
        Console.WriteLine(obrisan == null ? "Posetilac nije pronadjen." : "Posetilac je obrisan.");
    }

    private void PrikaziListuZelja()
    {
        int id = Unos.CeoBroj("Id posetioca: ");
        Posetilac? posetilac = _posetilacServis.PronadjiPoId(id);
        if (posetilac == null)
        {
            Console.WriteLine("Posetilac nije pronadjen.");
            return;
        }

        if (posetilac.ListaZelja.Count == 0)
        {
            Console.WriteLine("Lista zelja je prazna.");
            return;
        }

        foreach (Knjiga knjiga in posetilac.ListaZelja)
        {
            Console.WriteLine($"[{knjiga.Id}] {knjiga}");
        }
    }

    private void DodajNaListuZelja()
    {
        int posetilacId = Unos.CeoBroj("Id posetioca: ");
        foreach (Knjiga knjiga in _knjigaServis.DobaviSve())
        {
            Console.WriteLine($"[{knjiga.Id}] {knjiga.Naziv}");
        }

        int knjigaId = Unos.CeoBroj("Id knjige: ");
        _posetilacServis.DodajNaListuZelja(posetilacId, knjigaId);
        Console.WriteLine("Knjiga je dodata na listu zelja.");
    }

    private void UkloniSaListeZelja()
    {
        int posetilacId = Unos.CeoBroj("Id posetioca: ");
        int knjigaId = Unos.CeoBroj("Id knjige: ");
        _posetilacServis.UkloniSaListeZelja(posetilacId, knjigaId);
        Console.WriteLine("Knjiga je uklonjena sa liste zelja.");
    }

    private Posetilac UnesiPosetioca()
    {
        string ime = Unos.Tekst("Ime: ");
        string prezime = Unos.Tekst("Prezime: ");
        DateTime datumRodjenja = Unos.Datum("Datum rodjenja (dd.MM.yyyy): ");

        foreach (Adresa adresa in _adresaServis.DobaviSve())
        {
            Console.WriteLine($"[{adresa.Id}] {adresa}");
        }

        int adresaId = Unos.CeoBroj("Id adrese: ");
        string telefon = Unos.Tekst("Kontakt telefon: ");
        string email = Unos.Tekst("Email: ");
        string brojKarte = Unos.Tekst("Broj clanske karte (npr. SK-1-2023): ");
        int godinaClanstva = Unos.CeoBroj("Trenutna godina clanstva: ", 1900, 2100);
        string status = Unos.Tekst("Status (R - redovan, V - VIP): ").ToUpper();

        return new Posetilac(ime, prezime, datumRodjenja, adresaId, telefon, email, brojKarte, godinaClanstva,
            status == "V" ? StatusPosetioca.VIP : StatusPosetioca.Redovan);
    }
}
