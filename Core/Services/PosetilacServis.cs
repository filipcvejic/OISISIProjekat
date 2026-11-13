using Core.Model;

namespace Core.Services;

public class PosetilacServis
{
    private readonly PodaciSajma _podaci;

    public PosetilacServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Posetilac> DobaviSve()
    {
        return _podaci.Posetioci.DobaviSve();
    }

    public Posetilac? PronadjiPoId(int id)
    {
        return _podaci.Posetioci.PronadjiPoId(id);
    }

    public Posetilac Dodaj(Posetilac posetilac)
    {
        Proveri(posetilac);
        _podaci.Posetioci.Dodaj(posetilac);
        _podaci.Povezi();
        return posetilac;
    }

    public Posetilac? Izmeni(Posetilac posetilac)
    {
        Proveri(posetilac);
        Posetilac? izmenjen = _podaci.Posetioci.Izmeni(posetilac);
        _podaci.Povezi();
        return izmenjen;
    }

    public Posetilac? Obrisi(int id)
    {
        Posetilac? posetilac = _podaci.Posetioci.PronadjiPoId(id);
        if (posetilac == null) return null;

        foreach (Kupovina kupovina in _podaci.Kupovine.DobaviZaPosetioca(id))
        {
            _podaci.Kupovine.Obrisi(kupovina.Id);
        }

        _podaci.ListaZelja.ObrisiZaPosetioca(id);
        _podaci.Posetioci.Obrisi(id);
        _podaci.Povezi();
        return posetilac;
    }

    public void DodajNaListuZelja(int posetilacId, int knjigaId)
    {
        if (_podaci.Posetioci.PronadjiPoId(posetilacId) == null)
            throw new InvalidOperationException("Posetilac ne postoji.");
        if (_podaci.Knjige.PronadjiPoId(knjigaId) == null)
            throw new InvalidOperationException("Knjiga ne postoji.");
        if (_podaci.Kupovine.DobaviZaPosetioca(posetilacId).Exists(k => k.KnjigaId == knjigaId))
            throw new InvalidOperationException("Posetilac je vec kupio ovu knjigu.");
        if (!_podaci.ListaZelja.Dodaj(new StavkaListeZelja(posetilacId, knjigaId)))
            throw new InvalidOperationException("Knjiga je vec na listi zelja.");

        _podaci.Povezi();
    }

    public void UkloniSaListeZelja(int posetilacId, int knjigaId)
    {
        if (!_podaci.ListaZelja.Obrisi(posetilacId, knjigaId))
            throw new InvalidOperationException("Knjiga nije na listi zelja.");

        _podaci.Povezi();
    }

    private void Proveri(Posetilac posetilac)
    {
        if (_podaci.Adrese.PronadjiPoId(posetilac.AdresaId) == null)
            throw new InvalidOperationException("Adresa ne postoji.");

        bool zauzetBroj = _podaci.Posetioci.DobaviSve()
            .Exists(p => p.BrojClanskeKarte == posetilac.BrojClanskeKarte && p.Id != posetilac.Id);
        if (zauzetBroj)
            throw new InvalidOperationException("Broj clanske karte vec postoji.");
    }
}
