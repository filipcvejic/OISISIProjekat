using Core.Model;
using Core.Storage;
 
namespace Core.DAO;
 
// Preuzeto sa vezbi (CRUDExample)
public class KupovinaDAO
{
    private readonly List<Kupovina> _kupovine;
    private readonly Storage<Kupovina> _storage;
 
    public KupovinaDAO()
    {
        _storage = new Storage<Kupovina>("kupovine.txt");
        _kupovine = _storage.Load();
    }
 
    private int GenerisiId()
    {
        if (_kupovine.Count == 0) return 0;
        return _kupovine.Max(k => k.Id) + 1;
    }
 
    public Kupovina Dodaj(Kupovina kupovina)
    {
        kupovina.Id = GenerisiId();
        _kupovine.Add(kupovina);
        _storage.Save(_kupovine);
        return kupovina;
    }
 
    public Kupovina? Izmeni(Kupovina kupovina)
    {
        Kupovina? stara = PronadjiPoId(kupovina.Id);
        if (stara == null) return null;
 
        stara.PosetilacId = kupovina.PosetilacId;
        stara.KnjigaId = kupovina.KnjigaId;
        stara.DatumKupovine = kupovina.DatumKupovine;
        stara.Ocena = kupovina.Ocena;
        stara.Komentar = kupovina.Komentar;
 
        _storage.Save(_kupovine);
        return stara;
    }
 
    public Kupovina? Obrisi(int id)
    {
        Kupovina? kupovina = PronadjiPoId(id);
        if (kupovina == null) return null;
 
        _kupovine.Remove(kupovina);
        _storage.Save(_kupovine);
        return kupovina;
    }
 
    public Kupovina? PronadjiPoId(int id)
    {
        return _kupovine.Find(k => k.Id == id);
    }
 
    public List<Kupovina> DobaviSve()
    {
        return _kupovine;
    }
 
    public List<Kupovina> DobaviZaPosetioca(int posetilacId)
    {
        return _kupovine.FindAll(k => k.PosetilacId == posetilacId);
    }
 
    public List<Kupovina> DobaviZaKnjigu(int knjigaId)
    {
        return _kupovine.FindAll(k => k.KnjigaId == knjigaId);
    }
}