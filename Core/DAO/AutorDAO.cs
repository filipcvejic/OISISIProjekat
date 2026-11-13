using Core.Model;
using Core.Storage;

namespace Core.DAO;

// Preuzeto sa vezbi (CRUDExample)
public class AutorDAO
{
    private readonly List<Autor> _autori;
    private readonly Storage<Autor> _storage;

    public AutorDAO()
    {
        _storage = new Storage<Autor>("autori.txt");
        _autori = _storage.Load();
    }

    private int GenerisiId()
    {
        if (_autori.Count == 0) return 0;
        return _autori.Max(a => a.Id) + 1;
    }

    public Autor Dodaj(Autor autor)
    {
        autor.Id = GenerisiId();
        _autori.Add(autor);
        _storage.Save(_autori);
        return autor;
    }

    public Autor? Izmeni(Autor autor)
    {
        Autor? stari = PronadjiPoId(autor.Id);
        if (stari == null) return null;

        stari.Ime = autor.Ime;
        stari.Prezime = autor.Prezime;
        stari.DatumRodjenja = autor.DatumRodjenja;
        stari.AdresaId = autor.AdresaId;
        stari.Telefon = autor.Telefon;
        stari.Email = autor.Email;
        stari.BrojLicneKarte = autor.BrojLicneKarte;
        stari.GodineIskustva = autor.GodineIskustva;

        _storage.Save(_autori);
        return stari;
    }

    public Autor? Obrisi(int id)
    {
        Autor? autor = PronadjiPoId(id);
        if (autor == null) return null;

        _autori.Remove(autor);
        _storage.Save(_autori);
        return autor;
    }

    public Autor? PronadjiPoId(int id)
    {
        return _autori.Find(a => a.Id == id);
    }

    public List<Autor> DobaviSve()
    {
        return _autori;
    }
}

