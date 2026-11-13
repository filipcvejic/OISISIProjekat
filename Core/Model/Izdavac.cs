using Core.Serialization;

namespace Core.Model;

public class Izdavac : ISerializable
{
    public int Id { get; set; }
    public string Sifra { get; set; }
    public string Naziv { get; set; }
    public int? SefId { get; set; }
    public Autor? Sef { get; set; }
    public List<Autor> Autori { get; set; }
    public List<Knjiga> Knjige { get; set; }

    public Izdavac()
    {
        Sifra = string.Empty;
        Naziv = string.Empty;
        Autori = new List<Autor>();
        Knjige = new List<Knjiga>();
    }

    public Izdavac(string sifra, string naziv, int? sefId) : this()
    {
        Sifra = sifra;
        Naziv = naziv;
        SefId = sefId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Sifra,
            Naziv,
            SefId?.ToString() ?? string.Empty
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Sifra = values[1];
        Naziv = values[2];
        SefId = string.IsNullOrEmpty(values[3]) ? null : int.Parse(values[3]);
    }

    public override string ToString()
    {
        string sef = Sef != null ? $"{Sef.Ime} {Sef.Prezime}" : "-";
        string autori = Autori.Count == 0 ? "-" : string.Join(", ", Autori.Select(a => $"{a.Ime} {a.Prezime}"));
        string knjige = Knjige.Count == 0 ? "-" : string.Join(", ", Knjige.Select(k => k.Naziv));
        return $"{Sifra,-8} | {Naziv} | sef: {sef} | autori: {autori} | knjige: {knjige}";
    }
}
