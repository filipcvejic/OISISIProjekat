using Core.Model;

namespace Core.Services;

public class AdresaServis
{
    private readonly PodaciSajma _podaci;

    public AdresaServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Adresa> DobaviSve()
    {
        return _podaci.Adrese.DobaviSve();
    }

    public Adresa? PronadjiPoId(int id)
    {
        return _podaci.Adrese.PronadjiPoId(id);
    }

    public Adresa Dodaj(Adresa adresa)
    {
        return _podaci.Adrese.Dodaj(adresa);
    }

    public Adresa? Izmeni(Adresa adresa)
    {
        return _podaci.Adrese.Izmeni(adresa);
    }

    public Adresa? Obrisi(int id)
    {
        bool koristiSe = _podaci.Posetioci.DobaviSve().Exists(p => p.AdresaId == id)
                         || _podaci.Autori.DobaviSve().Exists(a => a.AdresaId == id);
        if (koristiSe)
            throw new InvalidOperationException("Adresa se koristi i ne moze da se obrise.");

        return _podaci.Adrese.Obrisi(id);
    }
}