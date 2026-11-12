using System.Globalization;
using Core.Serialization;

namespace Core.Model;

public class Knjiga : ISerializable
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Naziv { get; set; }
    public string Zanr { get; set; }
    public int GodinaIzdanja { get; set; }
    public decimal Cena { get; set; }
    public int BrojStrana { get; set; }
    public int? IzdavacId { get; set; }
    public Izdavac? Izdavac { get; set; }
    public List<Autor> Autori { get; set; }
    public List<Posetilac> Kupci { get; set; }
    public List<Posetilac> PosetiociListaZelja { get; set; }

    public Knjiga()
    {
        Isbn = string.Empty;
        Naziv = string.Empty;
        Zanr = string.Empty;
        Autori = new List<Autor>();
        Kupci = new List<Posetilac>();
        PosetiociListaZelja = new List<Posetilac>();
    }

    public Knjiga(string isbn, string naziv, string zanr, int godinaIzdanja, decimal cena, int brojStrana,
        int? izdavacId) : this()
    {
        Isbn = isbn;
        Naziv = naziv;
        Zanr = zanr;
        GodinaIzdanja = godinaIzdanja;
        Cena = cena;
        BrojStrana = brojStrana;
        IzdavacId = izdavacId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Isbn,
            Naziv,
            Zanr,
            GodinaIzdanja.ToString(),
            Cena.ToString(CultureInfo.InvariantCulture),
            BrojStrana.ToString(),
            IzdavacId?.ToString() ?? string.Empty
        };
        return csvValues;
    }
    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Isbn = values[1];
        Naziv = values[2];
        Zanr = values[3];
        GodinaIzdanja = int.Parse(values[4]);
        Cena = decimal.Parse(values[5], CultureInfo.InvariantCulture);
        BrojStrana = int.Parse(values[6]);
        IzdavacId = string.IsNullOrEmpty(values[7]) ? null : int.Parse(values[7]);
    }

    public override string ToString()
    {
        string izdavac = Izdavac?.Naziv ?? "-";
        string autori = Autori.Count == 0 ? "-" : string.Join(", ", Autori.Select(a => $"{a.Ime} {a.Prezime}"));
        return $"{Isbn,-17} | {Naziv} | {Zanr} | {GodinaIzdanja} | {Cena:0.00} din | {BrojStrana} str. | " +
               $"izdavac: {izdavac} | autori: {autori}";
    }
}