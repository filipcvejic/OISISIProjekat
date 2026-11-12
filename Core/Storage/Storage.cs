using Core.Serialization;

namespace Core.Storage;

// Preuzeto sa vezbi (CRUDExample)
public class Storage<T> where T : ISerializable, new()
{
    private static readonly string DataDirectory =
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Data"));

    private readonly string _filePath;
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _filePath = DataDirectory + Path.DirectorySeparatorChar + fileName;
    }

    public List<T> Load()
    {
        if (!Directory.Exists(DataDirectory))
        {
            Directory.CreateDirectory(DataDirectory);
        }

        if (!File.Exists(_filePath))
        {
            FileStream fs = File.Create(_filePath);
            fs.Close();
        }

        IEnumerable<string> lines = File.ReadLines(_filePath);
        return _serializer.FromCSV(lines);
    }

    public void Save(List<T> objects)
    {
        string serializedObjects = _serializer.ToCSV(objects);
        using (StreamWriter streamWriter = new StreamWriter(_filePath))
        {
            streamWriter.Write(serializedObjects);
        }
    }
}