using Core.Model;
using Core.Storage;
 
namespace Core.DAO;
 
// Preuzeto sa vezbi (CRUDExample)
public class IzdavacDAO
{
    private readonly List<Izdavac> _izdavaci;
    private readonly Storage<Izdavac> _storage;
 
    public IzdavacDAO()
    {
        _storage = new Storage<Izdavac>("izdavaci.txt");
        _izdavaci = _storage.Load();
    }
 
    private int GenerisiId()
    {
        if (_izdavaci.Count == 0) return 0;
        return _izdavaci.Max(i => i.Id) + 1;
    }
 
    public Izdavac Dodaj(Izdavac izdavac)
    {
        izdavac.Id = GenerisiId();
        _izdavaci.Add(izdavac);
        _storage.Save(_izdavaci);
        return izdavac;
    }
 
    public Izdavac? Izmeni(Izdavac izdavac)
    {
        Izdavac? stari = PronadjiPoId(izdavac.Id);
        if (stari == null) return null;
 
        stari.Sifra = izdavac.Sifra;
        stari.Naziv = izdavac.Naziv;
        stari.SefId = izdavac.SefId;
 
        _storage.Save(_izdavaci);
        return stari;
    }
 
    public Izdavac? Obrisi(int id)
    {
        Izdavac? izdavac = PronadjiPoId(id);
        if (izdavac == null) return null;
 
        _izdavaci.Remove(izdavac);
        _storage.Save(_izdavaci);
        return izdavac;
    }
 
    public Izdavac? PronadjiPoId(int id)
    {
        return _izdavaci.Find(i => i.Id == id);
    }
 
    public List<Izdavac> DobaviSve()
    {
        return _izdavaci;
    }
 
    public void UkloniSefa(int autorId)
    {
        foreach (Izdavac izdavac in _izdavaci.FindAll(i => i.SefId == autorId))
        {
            izdavac.SefId = null;
            izdavac.Sef = null;
        }
 
        _storage.Save(_izdavaci);
    }
}