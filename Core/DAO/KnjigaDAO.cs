using Core.Model;
using Core.Storage;

namespace Core.DAO;

// Preuzeto sa vezbi (CRUDExample)
public class KnjigaDAO
{
    private readonly List<Knjiga> _knjige;
    private readonly Storage<Knjiga> _storage;

    public KnjigaDAO()
    {
        _storage = new Storage<Knjiga>("knjige.txt");
        _knjige = _storage.Load();
    }

    private int GenerisiId()
    {
        if (_knjige.Count == 0) return 0;
        return _knjige.Max(k => k.Id) + 1;
    }

    public Knjiga Dodaj(Knjiga knjiga)
    {
        knjiga.Id = GenerisiId();
        _knjige.Add(knjiga);
        _storage.Save(_knjige);
        return knjiga;
    }

    public Knjiga? Izmeni(Knjiga knjiga)
    {
        Knjiga? stara = PronadjiPoId(knjiga.Id);
        if (stara == null) return null;

        stara.Isbn = knjiga.Isbn;
        stara.Naziv = knjiga.Naziv;
        stara.Zanr = knjiga.Zanr;
        stara.GodinaIzdanja = knjiga.GodinaIzdanja;
        stara.Cena = knjiga.Cena;
        stara.BrojStrana = knjiga.BrojStrana;
        stara.IzdavacId = knjiga.IzdavacId;

        _storage.Save(_knjige);
        return stara;
    }

    public Knjiga? Obrisi(int id)
    {
        Knjiga? knjiga = PronadjiPoId(id);
        if (knjiga == null) return null;

        _knjige.Remove(knjiga);
        _storage.Save(_knjige);
        return knjiga;
    }

    public Knjiga? PronadjiPoId(int id)
    {
        return _knjige.Find(k => k.Id == id);
    }

    public List<Knjiga> DobaviSve()
    {
        return _knjige;
    }

    public void UkloniIzdavaca(int izdavacId)
    {
        foreach (Knjiga knjiga in _knjige.FindAll(k => k.IzdavacId == izdavacId))
        {
            knjiga.IzdavacId = null;
            knjiga.Izdavac = null;
        }

        _storage.Save(_knjige);
    }
}

