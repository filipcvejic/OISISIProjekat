using Core.Model;

namespace Core.Services;

public class KnjigaServis
{
    private readonly PodaciSajma _podaci;

    public KnjigaServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Knjiga> DobaviSve()
    {
        return _podaci.Knjige.DobaviSve();
    }

    public Knjiga? PronadjiPoId(int id)
    {
        return _podaci.Knjige.PronadjiPoId(id);
    }

    public Knjiga Dodaj(Knjiga knjiga, List<int> autorIds)
    {
        Proveri(knjiga);

        if (autorIds.Count == 0)
            throw new InvalidOperationException("Knjiga mora imati bar jednog autora.");
        if (autorIds.Exists(id => _podaci.Autori.PronadjiPoId(id) == null))
            throw new InvalidOperationException("Neki od autora ne postoji.");

        _podaci.Knjige.Dodaj(knjiga);
        foreach (int autorId in autorIds.Distinct())
        {
            _podaci.KnjigeAutori.Dodaj(new KnjigaAutor(knjiga.Id, autorId));
        }

        _podaci.Povezi();
        return knjiga;
    }

    public Knjiga? Izmeni(Knjiga knjiga)
    {
        Proveri(knjiga);
        Knjiga? izmenjena = _podaci.Knjige.Izmeni(knjiga);
        _podaci.Povezi();
        return izmenjena;
    }

    public Knjiga? Obrisi(int id)
    {
        Knjiga? knjiga = _podaci.Knjige.PronadjiPoId(id);
        if (knjiga == null) return null;

        foreach (Kupovina kupovina in _podaci.Kupovine.DobaviZaKnjigu(id))
        {
            _podaci.Kupovine.Obrisi(kupovina.Id);
        }

        _podaci.ListaZelja.ObrisiZaKnjigu(id);
        _podaci.KnjigeAutori.ObrisiZaKnjigu(id);
        _podaci.Knjige.Obrisi(id);
        _podaci.Povezi();
        return knjiga;
    }
    public void DodajAutora(int knjigaId, int autorId)
    {
        if (_podaci.Knjige.PronadjiPoId(knjigaId) == null)
            throw new InvalidOperationException("Knjiga ne postoji.");
        if (_podaci.Autori.PronadjiPoId(autorId) == null)
            throw new InvalidOperationException("Autor ne postoji.");
        if (!_podaci.KnjigeAutori.Dodaj(new KnjigaAutor(knjigaId, autorId)))
            throw new InvalidOperationException("Autor je vec autor ove knjige.");

        _podaci.Povezi();
    }

    public void UkloniAutora(int knjigaId, int autorId)
    {
        Knjiga? knjiga = _podaci.Knjige.PronadjiPoId(knjigaId);
        if (knjiga == null)
            throw new InvalidOperationException("Knjiga ne postoji.");
        if (knjiga.Autori.Count == 1 && knjiga.Autori[0].Id == autorId)
            throw new InvalidOperationException("Knjiga mora imati bar jednog autora.");
        if (!_podaci.KnjigeAutori.Obrisi(knjigaId, autorId))
            throw new InvalidOperationException("Autor nije autor ove knjige.");

        _podaci.Povezi();
    }

    private void Proveri(Knjiga knjiga)
    {
        bool zauzetIsbn = _podaci.Knjige.DobaviSve()
            .Exists(k => k.Isbn == knjiga.Isbn && k.Id != knjiga.Id);
        if (zauzetIsbn)
            throw new InvalidOperationException("ISBN vec postoji.");

        if (knjiga.IzdavacId.HasValue && _podaci.Izdavaci.PronadjiPoId(knjiga.IzdavacId.Value) == null)
            throw new InvalidOperationException("Izdavac ne postoji.");
    }
}