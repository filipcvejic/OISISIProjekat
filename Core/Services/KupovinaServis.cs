using Core.Model;

namespace Core.Services;

public class KupovinaServis
{
    private readonly PodaciSajma _podaci;

    public KupovinaServis(PodaciSajma podaci)
    {
        _podaci = podaci;
    }

    public List<Kupovina> DobaviSve()
    {
        return _podaci.Kupovine.DobaviSve();
    }

    public Kupovina? PronadjiPoId(int id)
    {
        return _podaci.Kupovine.PronadjiPoId(id);
    }

    public Kupovina Dodaj(Kupovina kupovina)
    {
        Proveri(kupovina);
        _podaci.Kupovine.Dodaj(kupovina);
        _podaci.ListaZelja.Obrisi(kupovina.PosetilacId, kupovina.KnjigaId);
        _podaci.Povezi();
        return kupovina;
    }

    public Kupovina? Izmeni(Kupovina kupovina)
    {
        Proveri(kupovina);
        Kupovina? izmenjena = _podaci.Kupovine.Izmeni(kupovina);
        _podaci.Povezi();
        return izmenjena;
    }

    public Kupovina? Obrisi(int id)
    {
        Kupovina? kupovina = _podaci.Kupovine.Obrisi(id);
        _podaci.Povezi();
        return kupovina;
    }

    private void Proveri(Kupovina kupovina)
    {
        if (_podaci.Posetioci.PronadjiPoId(kupovina.PosetilacId) == null)
            throw new InvalidOperationException("Posetilac ne postoji.");
        if (_podaci.Knjige.PronadjiPoId(kupovina.KnjigaId) == null)
            throw new InvalidOperationException("Knjiga ne postoji.");
        if (kupovina.Ocena < 1 || kupovina.Ocena > 5)
            throw new InvalidOperationException("Ocena mora biti od 1 do 5.");
    }
}