using Core.Model;
using Core.Storage;

namespace Core.DAO;

public class KnjigaAutorDAO
{
    private readonly List<KnjigaAutor> _veze;
    private readonly Storage<KnjigaAutor> _storage;

    public KnjigaAutorDAO()
    {
        _storage = new Storage<KnjigaAutor>("knjige_autori.txt");
        _veze = _storage.Load();
    }

    public bool Dodaj(KnjigaAutor veza)
    {
        if (Postoji(veza.KnjigaId, veza.AutorId)) return false;

        _veze.Add(veza);
        _storage.Save(_veze);
        return true;
    }

    public bool Obrisi(int knjigaId, int autorId)
    {
        int obrisano = _veze.RemoveAll(v => v.KnjigaId == knjigaId && v.AutorId == autorId);
        if (obrisano == 0) return false;

        _storage.Save(_veze);
        return true;
    }

    public void ObrisiZaKnjigu(int knjigaId)
    {
        _veze.RemoveAll(v => v.KnjigaId == knjigaId);
        _storage.Save(_veze);
    }

    public void ObrisiZaAutora(int autorId)
    {
        _veze.RemoveAll(v => v.AutorId == autorId);
        _storage.Save(_veze);
    }

    public bool Postoji(int knjigaId, int autorId)
    {
        return _veze.Exists(v => v.KnjigaId == knjigaId && v.AutorId == autorId);
    }

    public List<KnjigaAutor> DobaviSve()
    {
        return _veze;
    }
}
