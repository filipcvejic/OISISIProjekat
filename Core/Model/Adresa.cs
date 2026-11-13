using Core.Serialization;

namespace Core.Model;

public class Adresa : ISerializable
{
    public int Id { get; set; }
    public string Ulica { get; set; }
    public string Broj { get; set; }
    public string Grad { get; set; }
    public string Drzava { get; set; }

    public Adresa()
    {
        Ulica = string.Empty;
        Broj = string.Empty;
        Grad = string.Empty;
        Drzava = string.Empty;
    }

    public Adresa(string ulica, string broj, string grad, string drzava)
    {
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ulica,
            Broj,
            Grad,
            Drzava
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Ulica = values[1];
        Broj = values[2];
        Grad = values[3];
        Drzava = values[4];
    }

    public override string ToString()
    {
        return $"{Ulica} {Broj}, {Grad}, {Drzava}";
    }
}