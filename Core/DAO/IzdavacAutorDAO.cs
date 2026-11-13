using Core.Model;
using Core.Storage;
 
namespace Core.DAO;
 
public class IzdavacAutorDAO
{
    private readonly List<IzdavacAutor> _veze;
    private readonly Storage<IzdavacAutor> _storage;
 
    public IzdavacAutorDAO()
    {
        _storage = new Storage<IzdavacAutor>("izdavaci_autori.txt");
        _veze = _storage.Load();
    }
 
    public bool Dodaj(IzdavacAutor veza)
    {
        if (Postoji(veza.IzdavacId, veza.AutorId)) return false;
 
        _veze.Add(veza);
        _storage.Save(_veze);
        return true;
    }
 
    public bool Obrisi(int izdavacId, int autorId)
    {
        int obrisano = _veze.RemoveAll(v => v.IzdavacId == izdavacId && v.AutorId == autorId);
        if (obrisano == 0) return false;
 
        _storage.Save(_veze);
        return true;
    }
 
    public void ObrisiZaIzdavaca(int izdavacId)
    {
        _veze.RemoveAll(v => v.IzdavacId == izdavacId);
        _storage.Save(_veze);
    }
 
    public void ObrisiZaAutora(int autorId)
    {
        _veze.RemoveAll(v => v.AutorId == autorId);
        _storage.Save(_veze);
    }
 
    public bool Postoji(int izdavacId, int autorId)
    {
        return _veze.Exists(v => v.IzdavacId == izdavacId && v.AutorId == autorId);
    }
 
    public List<IzdavacAutor> DobaviSve()
    {
        return _veze;
    }
}