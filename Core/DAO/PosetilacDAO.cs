using Core.Model;
using Core.Storage;

namespace Core.DAO;

// Preuzeto sa vezbi (CRUDExample)
public class PosetilacDAO
{
    private readonly List<Posetilac> _posetioci;
    private readonly Storage<Posetilac> _storage;

    public PosetilacDAO()
    {
        _storage = new Storage<Posetilac>("posetioci.txt");
        _posetioci = _storage.Load();
    }

    private int GenerisiId()
    {
        if (_posetioci.Count == 0) return 0;
        return _posetioci.Max(p => p.Id) + 1;
    }

    public Posetilac Dodaj(Posetilac posetilac)
    {
        posetilac.Id = GenerisiId();
        _posetioci.Add(posetilac);
        _storage.Save(_posetioci);
        return posetilac;
    }

    public Posetilac? Izmeni(Posetilac posetilac)
    {
        Posetilac? stari = PronadjiPoId(posetilac.Id);
        if (stari == null) return null;

        stari.Ime = posetilac.Ime;
        stari.Prezime = posetilac.Prezime;
        stari.DatumRodjenja = posetilac.DatumRodjenja;
        stari.AdresaId = posetilac.AdresaId;
        stari.KontaktTelefon = posetilac.KontaktTelefon;
        stari.Email = posetilac.Email;
        stari.BrojClanskeKarte = posetilac.BrojClanskeKarte;
        stari.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
        stari.Status = posetilac.Status;

        _storage.Save(_posetioci);
        return stari;
    }

    public Posetilac? Obrisi(int id)
    {
        Posetilac? posetilac = PronadjiPoId(id);
        if (posetilac == null) return null;

        _posetioci.Remove(posetilac);
        _storage.Save(_posetioci);
        return posetilac;
    }

    public Posetilac? PronadjiPoId(int id)
    {
        return _posetioci.Find(p => p.Id == id);
    }

    public List<Posetilac> DobaviSve()
    {
        return _posetioci;
    }
}