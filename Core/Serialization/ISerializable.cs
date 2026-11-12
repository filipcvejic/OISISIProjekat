namespace Core.Serialization;

// Preuzeto sa vezbi (SerializationExample)
public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}
