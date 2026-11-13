using Core.Model;

namespace Core.Services;

public class AutorServis
{
    private readonly PodaciSajma _podaci;

    public AutorServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Autor> DobaviSve()
    {
        return _podaci.Autori.DobaviSve();
    }

    public Autor? PronadjiPoId(int id)
    {
        return _podaci.Autori.PronadjiPoId(id);
    }

    public Autor Dodaj(Autor autor)
    {
        Proveri(autor);
        _podaci.Autori.Dodaj(autor);
        _podaci.Povezi();
        return autor;
    }

    public Autor? Izmeni(Autor autor)
    {
        Proveri(autor);

        bool jeSef = _podaci.Izdavaci.DobaviSve().Exists(i => i.SefId == autor.Id);
        if (jeSef && autor.GodineIskustva < 5)
            throw new InvalidOperationException("Autor je sef izdavaca i mora imati bar 5 godina iskustva.");

        Autor? izmenjen = _podaci.Autori.Izmeni(autor);
        _podaci.Povezi();
        return izmenjen;
    }

    public Autor? Obrisi(int id)
    {
        Autor? autor = _podaci.Autori.PronadjiPoId(id);
        if (autor == null) return null;

        bool jedini = autor.Knjige.Exists(k => k.Autori.Count == 1);
        if (jedini)
            throw new InvalidOperationException("Autor je jedini autor neke knjige i ne moze da se obrise.");

        _podaci.KnjigeAutori.ObrisiZaAutora(id);
        _podaci.IzdavaciAutori.ObrisiZaAutora(id);
        _podaci.Izdavaci.UkloniSefa(id);
        _podaci.Autori.Obrisi(id);
        _podaci.Povezi();
        return autor;
    }

    private void Proveri(Autor autor)
    {
        if (_podaci.Adrese.PronadjiPoId(autor.AdresaId) == null)
            throw new InvalidOperationException("Adresa ne postoji.");
    }
}