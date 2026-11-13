using Core.Model;
using Core.Storage;

namespace Core.DAO;

public class StavkaListeZeljaDAO
{
    private readonly List<StavkaListeZelja> _veze;
    private readonly Storage<StavkaListeZelja> _storage;

    public StavkaListeZeljaDAO()
    {
        _storage = new Storage<StavkaListeZelja>("lista_zelja.txt");
        _veze = _storage.Load();
    }

    public bool Dodaj(StavkaListeZelja veza)
    {
        if (Postoji(veza.PosetilacId, veza.KnjigaId)) return false;

        _veze.Add(veza);
        _storage.Save(_veze);
        return true;
    }

    public bool Obrisi(int posetilacId, int knjigaId)
    {
        int obrisano = _veze.RemoveAll(v => v.PosetilacId == posetilacId && v.KnjigaId == knjigaId);
        if (obrisano == 0) return false;

        _storage.Save(_veze);
        return true;
    }

    public void ObrisiZaPosetioca(int posetilacId)
    {
        _veze.RemoveAll(v => v.PosetilacId == posetilacId);
        _storage.Save(_veze);
    }

    public void ObrisiZaKnjigu(int knjigaId)
    {
        _veze.RemoveAll(v => v.KnjigaId == knjigaId);
        _storage.Save(_veze);
    }

    public bool Postoji(int posetilacId, int knjigaId)
    {
        return _veze.Exists(v => v.PosetilacId == posetilacId && v.KnjigaId == knjigaId);
    }

    public List<StavkaListeZelja> DobaviSve()
    {
        return _veze;
    }
}
