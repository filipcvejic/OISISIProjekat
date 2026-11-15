using Core.Model;
using Core.Services;

namespace ConsoleClient.View;

public class KupovinaConsoleView
{
    private readonly KupovinaServis _kupovinaServis;
    private readonly PosetilacServis _posetilacServis;
    private readonly KnjigaServis _knjigaServis;

    public KupovinaConsoleView(KupovinaServis kupovinaServis, PosetilacServis posetilacServis,
        KnjigaServis knjigaServis)
    {
        _kupovinaServis = kupovinaServis;
        _posetilacServis = posetilacServis;
        _knjigaServis = knjigaServis;
    }

    public void Pokreni()
    {
        while (true)
        {
            Console.WriteLine("\n--- KUPOVINE ---");
            Console.WriteLine("1: Prikazi sve kupovine");
            Console.WriteLine("2: Dodaj kupovinu");
            Console.WriteLine("3: Izmeni kupovinu");
            Console.WriteLine("4: Obrisi kupovinu");
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
        List<Kupovina> kupovine = _kupovinaServis.DobaviSve();
        if (kupovine.Count == 0)
        {
            Console.WriteLine("Nema kupovina.");
            return;
        }

        foreach (Kupovina kupovina in kupovine)
        {
            Console.WriteLine(kupovina);
        }
    }

    private void Dodaj()
    {
        Kupovina kupovina = UnesiKupovinu();
        _kupovinaServis.Dodaj(kupovina);
        Console.WriteLine("Kupovina je dodata.");
    }

    private void Izmeni()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id kupovine: ");
        if (_kupovinaServis.PronadjiPoId(id) == null)
        {
            Console.WriteLine("Kupovina nije pronadjena.");
            return;
        }

        Kupovina kupovina = UnesiKupovinu();
        kupovina.Id = id;
        _kupovinaServis.Izmeni(kupovina);
        Console.WriteLine("Kupovina je izmenjena.");
    }

    private void Obrisi()
    {
        PrikaziSve();
        int id = Unos.CeoBroj("Id kupovine: ");
        Kupovina? obrisana = _kupovinaServis.Obrisi(id);
        Console.WriteLine(obrisana == null ? "Kupovina nije pronadjena." : "Kupovina je obrisana.");
    }

    private Kupovina UnesiKupovinu()
    {
        foreach (Posetilac posetilac in _posetilacServis.DobaviSve())
        {
            Console.WriteLine($"[{posetilac.Id}] {posetilac.Ime} {posetilac.Prezime}");
        }

        int posetilacId = Unos.CeoBroj("Id posetioca: ");

        foreach (Knjiga knjiga in _knjigaServis.DobaviSve())
        {
            Console.WriteLine($"[{knjiga.Id}] {knjiga.Naziv}");
        }

        int knjigaId = Unos.CeoBroj("Id knjige: ");
        DateTime datum = Unos.Datum("Datum kupovine (dd.MM.yyyy): ");
        int ocena = Unos.CeoBroj("Ocena (1-5): ", 1, 5);
        string komentar = Unos.Tekst("Komentar: ");

        return new Kupovina(posetilacId, knjigaId, datum, ocena, komentar);
    }
}
