using Core.Model;

namespace Core.Services;

public class IzdavacServis
{
    private readonly PodaciSajma _podaci;

    public IzdavacServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Izdavac> DobaviSve()
    {
        return _podaci.Izdavaci.DobaviSve();
    }

    public Izdavac? PronadjiPoId(int id)
    {
        return _podaci.Izdavaci.PronadjiPoId(id);
    }

    public Izdavac Dodaj(Izdavac izdavac)
    {
        Proveri(izdavac);
        _podaci.Izdavaci.Dodaj(izdavac);
        _podaci.Povezi();
        return izdavac;
    }

    public Izdavac? Izmeni(Izdavac izdavac)
    {
        Proveri(izdavac);
        Izdavac? izmenjen = _podaci.Izdavaci.Izmeni(izdavac);
        _podaci.Povezi();
        return izmenjen;
    }

    public Izdavac? Obrisi(int id)
    {
        Izdavac? izdavac = _podaci.Izdavaci.PronadjiPoId(id);
        if (izdavac == null) return null;

        _podaci.Knjige.UkloniIzdavaca(id);
        _podaci.IzdavaciAutori.ObrisiZaIzdavaca(id);
        _podaci.Izdavaci.Obrisi(id);
        _podaci.Povezi();
        return izdavac;
    }

    public void DodajAutora(int izdavacId, int autorId)
    {
        if (_podaci.Izdavaci.PronadjiPoId(izdavacId) == null)
            throw new InvalidOperationException("Izdavac ne postoji.");
        if (_podaci.Autori.PronadjiPoId(autorId) == null)
            throw new InvalidOperationException("Autor ne postoji.");
        if (!_podaci.IzdavaciAutori.Dodaj(new IzdavacAutor(izdavacId, autorId)))
            throw new InvalidOperationException("Autor je vec kod ovog izdavaca.");

        _podaci.Povezi();
    }

    public void UkloniAutora(int izdavacId, int autorId)
    {
        if (!_podaci.IzdavaciAutori.Obrisi(izdavacId, autorId))
            throw new InvalidOperationException("Autor nije kod ovog izdavaca.");

        _podaci.Povezi();
    }

    private void Proveri(Izdavac izdavac)
    {
        bool zauzetaSifra = _podaci.Izdavaci.DobaviSve()
            .Exists(i => i.Sifra == izdavac.Sifra && i.Id != izdavac.Id);
        if (zauzetaSifra)
            throw new InvalidOperationException("Sifra izdavaca vec postoji.");

        if (izdavac.SefId.HasValue)
        {
            Autor? sef = _podaci.Autori.PronadjiPoId(izdavac.SefId.Value);
            if (sef == null)
                throw new InvalidOperationException("Sef ne postoji.");
            if (sef.GodineIskustva < 5)
                throw new InvalidOperationException("Sef mora imati bar 5 godina iskustva.");
        }
    }
}
