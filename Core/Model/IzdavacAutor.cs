using Core.Serialization;

namespace Core.Model;

public class IzdavacAutor : ISerializable
{
    public int IzdavacId { get; set; }
    public int AutorId { get; set; }

    public IzdavacAutor()
    {
    }

    public IzdavacAutor(int izdavacId, int autorId)
    {
        IzdavacId = izdavacId;
        AutorId = autorId;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            IzdavacId.ToString(),
            AutorId.ToString()
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        IzdavacId = int.Parse(values[0]);
        AutorId = int.Parse(values[1]);
    }
}
