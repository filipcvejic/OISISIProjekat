using Core.Model;
using Core.Storage;

namespace Core.DAO;

// Preuzeto sa vezbi (CRUDExample)
public class AdresaDAO
{
    private readonly List<Adresa> _adrese;
    private readonly Storage<Adresa> _storage;

    public AdresaDAO()
    {
        _storage = new Storage<Adresa>("adrese.txt");
        _adrese = _storage.Load();
    }

    private int GenerisiId()
    {
        if (_adrese.Count == 0) return 0;
        return _adrese.Max(a => a.Id) + 1;
    }

    public Adresa Dodaj(Adresa adresa)
    {
        adresa.Id = GenerisiId();
        _adrese.Add(adresa);
        _storage.Save(_adrese);
        return adresa;
    }

    public Adresa? Izmeni(Adresa adresa)
    {
        Adresa? stara = PronadjiPoId(adresa.Id);
        if (stara == null) return null;

        stara.Ulica = adresa.Ulica;
        stara.Broj = adresa.Broj;
        stara.Grad = adresa.Grad;
        stara.Drzava = adresa.Drzava;

        _storage.Save(_adrese);
        return stara;
    }

    public Adresa? Obrisi(int id)
    {
        Adresa? adresa = PronadjiPoId(id);
        if (adresa == null) return null;

        _adrese.Remove(adresa);
        _storage.Save(_adrese);
        return adresa;
    }

    public Adresa? PronadjiPoId(int id)
    {
        return _adrese.Find(a => a.Id == id);
    }

    public List<Adresa> DobaviSve()
    {
        return _adrese;
    }
}
