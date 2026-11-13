using System.Globalization;
using Core.Serialization;

namespace Core.Model;

public class Kupovina : ISerializable
{
    public int Id { get; set; }
    public int PosetilacId { get; set; }
    public Posetilac? Posetilac { get; set; }
    public int KnjigaId { get; set; }
    public Knjiga? Knjiga { get; set; }
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }
    public string Komentar { get; set; }

    public Kupovina()
    {
        Komentar = string.Empty;
    }

    public Kupovina(int posetilacId, int knjigaId, DateTime datumKupovine, int ocena, string komentar)
    {
        PosetilacId = posetilacId;
        KnjigaId = knjigaId;
        DatumKupovine = datumKupovine;
        Ocena = ocena;
        Komentar = komentar;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            PosetilacId.ToString(),
            KnjigaId.ToString(),
            DatumKupovine.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
            Ocena.ToString(),
            Komentar
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        PosetilacId = int.Parse(values[1]);
        KnjigaId = int.Parse(values[2]);
        DatumKupovine = DateTime.ParseExact(values[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
        Ocena = int.Parse(values[4]);
        Komentar = values[5];
    }

    public override string ToString()
    {
        string posetilac = Posetilac != null ? $"{Posetilac.Ime} {Posetilac.Prezime}" : PosetilacId.ToString();
        string knjiga = Knjiga?.Naziv ?? KnjigaId.ToString();
        return $"{Id,-4} | {posetilac} | {knjiga} | {DatumKupovine:dd.MM.yyyy} | ocena: {Ocena} | {Komentar}";
    }
}
