using Core.DAO;
using Core.Model;

namespace Core.Services;

public class PodaciSajma
{
    public AdresaDAO Adrese { get; }
    public PosetilacDAO Posetioci { get; }
    public AutorDAO Autori { get; }
    public KnjigaDAO Knjige { get; }
    public IzdavacDAO Izdavaci { get; }
    public KupovinaDAO Kupovine { get; }
    public KnjigaAutorDAO KnjigeAutori { get; }
    public IzdavacAutorDAO IzdavaciAutori { get; }
    public StavkaListeZeljaDAO ListaZelja { get; }

    public PodaciSajma()
    {
        Adrese = new AdresaDAO();
        Posetioci = new PosetilacDAO();
        Autori = new AutorDAO();
        Knjige = new KnjigaDAO();
        Izdavaci = new IzdavacDAO();
        Kupovine = new KupovinaDAO();
        KnjigeAutori = new KnjigaAutorDAO();
        IzdavaciAutori = new IzdavacAutorDAO();
        ListaZelja = new StavkaListeZeljaDAO();

        Povezi();
    }

    public void Povezi()
    {
        foreach (Posetilac posetilac in Posetioci.DobaviSve())
        {
            posetilac.Adresa = Adrese.PronadjiPoId(posetilac.AdresaId);
            posetilac.Kupovine.Clear();
            posetilac.ListaZelja.Clear();
        }

        foreach (Autor autor in Autori.DobaviSve())
        {
            autor.Adresa = Adrese.PronadjiPoId(autor.AdresaId);
            autor.Knjige.Clear();
        }

        foreach (Izdavac izdavac in Izdavaci.DobaviSve())
        {
            izdavac.Sef = izdavac.SefId.HasValue ? Autori.PronadjiPoId(izdavac.SefId.Value) : null;
            izdavac.Autori.Clear();
            izdavac.Knjige.Clear();
        }

        foreach (Knjiga knjiga in Knjige.DobaviSve())
        {
            knjiga.Izdavac = knjiga.IzdavacId.HasValue ? Izdavaci.PronadjiPoId(knjiga.IzdavacId.Value) : null;
            knjiga.Izdavac?.Knjige.Add(knjiga);
            knjiga.Autori.Clear();
            knjiga.Kupci.Clear();
            knjiga.PosetiociListaZelja.Clear();
        }

        foreach (Kupovina kupovina in Kupovine.DobaviSve())
        {
            kupovina.Posetilac = Posetioci.PronadjiPoId(kupovina.PosetilacId);
            kupovina.Knjiga = Knjige.PronadjiPoId(kupovina.KnjigaId);
            if (kupovina.Posetilac == null || kupovina.Knjiga == null) continue;

            kupovina.Posetilac.Kupovine.Add(kupovina);
            if (!kupovina.Knjiga.Kupci.Contains(kupovina.Posetilac))
            {
                kupovina.Knjiga.Kupci.Add(kupovina.Posetilac);
            }
        }

        foreach (KnjigaAutor veza in KnjigeAutori.DobaviSve())
        {
            Knjiga? knjiga = Knjige.PronadjiPoId(veza.KnjigaId);
            Autor? autor = Autori.PronadjiPoId(veza.AutorId);
            if (knjiga == null || autor == null) continue;

            knjiga.Autori.Add(autor);
            autor.Knjige.Add(knjiga);
        }

        foreach (IzdavacAutor veza in IzdavaciAutori.DobaviSve())
        {
            Izdavac? izdavac = Izdavaci.PronadjiPoId(veza.IzdavacId);
            Autor? autor = Autori.PronadjiPoId(veza.AutorId);
            if (izdavac == null || autor == null) continue;

            izdavac.Autori.Add(autor);
        }

        foreach (StavkaListeZelja stavka in ListaZelja.DobaviSve())
        {
            Posetilac? posetilac = Posetioci.PronadjiPoId(stavka.PosetilacId);
            Knjiga? knjiga = Knjige.PronadjiPoId(stavka.KnjigaId);
            if (posetilac == null || knjiga == null) continue;

            posetilac.ListaZelja.Add(knjiga);
            knjiga.PosetiociListaZelja.Add(posetilac);
        }
    }
}
