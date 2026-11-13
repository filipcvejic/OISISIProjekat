using System.Globalization;
using Core.Serialization;

namespace Core.Model;

public class Posetilac : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public DateTime DatumRodjenja { get; set; }
    public int AdresaId { get; set; }
    public Adresa? Adresa { get; set; }
    public string KontaktTelefon { get; set; }
    public string Email { get; set; }
    public string BrojClanskeKarte { get; set; }
    public int TrenutnaGodinaClanstva { get; set; }
    public StatusPosetioca Status { get; set; }
    public List<Kupovina> Kupovine { get; set; }
    public List<Knjiga> ListaZelja { get; set; }

    public double ProsecnaOcena => Kupovine.Count == 0 ? 0 : Kupovine.Average(k => k.Ocena);

    public Posetilac()
    {
        Ime = string.Empty;
        Prezime = string.Empty;
        KontaktTelefon = string.Empty;
        Email = string.Empty;
        BrojClanskeKarte = string.Empty;
        Kupovine = new List<Kupovina>();
        ListaZelja = new List<Knjiga>();
    }

    public Posetilac(string ime, string prezime, DateTime datumRodjenja, int adresaId, string kontaktTelefon,
        string email, string brojClanskeKarte, int trenutnaGodinaClanstva, StatusPosetioca status) : this()
    {
        Ime = ime;
        Prezime = prezime;
        DatumRodjenja = datumRodjenja;
        AdresaId = adresaId;
        KontaktTelefon = kontaktTelefon;
        Email = email;
        BrojClanskeKarte = brojClanskeKarte;
        TrenutnaGodinaClanstva = trenutnaGodinaClanstva;
        Status = status;
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
            KontaktTelefon,
            Email,
            BrojClanskeKarte,
            TrenutnaGodinaClanstva.ToString(),
            Status == StatusPosetioca.VIP ? "V" : "R"
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
        KontaktTelefon = values[5];
        Email = values[6];
        BrojClanskeKarte = values[7];
        TrenutnaGodinaClanstva = int.Parse(values[8]);
        Status = values[9] == "V" ? StatusPosetioca.VIP : StatusPosetioca.Redovan;
    }

    public override string ToString()
    {
        string adresa = Adresa?.ToString() ?? "-";
        return $"{BrojClanskeKarte,-12} | {Ime} {Prezime} | {DatumRodjenja:dd.MM.yyyy} | {adresa} | " +
               $"{KontaktTelefon} | {Email} | clan od: {TrenutnaGodinaClanstva} | {Status} | " +
               $"prosecna ocena: {ProsecnaOcena:0.00}";
    }
}
