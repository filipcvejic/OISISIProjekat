using System.Globalization;
using Core.Serialization;

namespace Core.Model;

public class Autor : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public int AdresaId { get; set; }
    public Adresa? Adresa { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string BrojLicneKarte { get; set; }
    public int GodineIskustva { get; set; }
    public List<Knjiga> Knjige { get; set; }

    public Autor()
    {
        Ime = string.Empty;
        Prezime = string.Empty;
        Telefon = string.Empty;
        Email = string.Empty;
        BrojLicneKarte = string.Empty;
        Knjige = new List<Knjiga>();
    }

    public Autor(string ime, string prezime, DateTime datumRodjenja, int adresaId, string telefon, string email,
        string brojLicneKarte, int godineIskustva) : this()
    {
        Ime = ime;
        Prezime = prezime;
        DatumRodjenja = datumRodjenja;
        AdresaId = adresaId;
        Telefon = telefon;
        Email = email;
        BrojLicneKarte = brojLicneKarte;
        GodineIskustva = godineIskustva;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ime,
            Prezime,
            DatumRodjenja.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
            AdresaId.ToString(),
            Telefon,
            Email,
            BrojLicneKarte,
            GodineIskustva.ToString()
        };
        return csvValues;
    }
    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ime = values[1];
        Prezime = values[2];
        DatumRodjenja = DateTime.ParseExact(values[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        AdresaId = int.Parse(values[4]);
        Telefon = values[5];
        Email = values[6];
        BrojLicneKarte = values[7];
        GodineIskustva = int.Parse(values[8]);
    }

    public override string ToString()
    {
        string adresa = Adresa?.ToString() ?? "-";
        string knjige = Knjige.Count == 0 ? "-" : string.Join(", ", Knjige.Select(k => k.Naziv));
        return $"{Id,-4} | {Ime} {Prezime} | LK: {BrojLicneKarte} | {DatumRodjenja:dd.MM.yyyy} | {adresa} | " +
               $"{Telefon} | {Email} | iskustvo: {GodineIskustva} god. | knjige: {knjige}";
    }
}