using System.Xml.Serialization;

namespace FileIO;

public class Save : ISave
{

    // if needed, do not forget to add the data object to the method parameters
    // and update the interface
    public Task SaveAsJson(string targetFilePath)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsXml<T>(T data, string targetFilePath)
    {
        if (data is null || targetFilePath.Equals(string.Empty))
            return;

        var xmlSerializer = new XmlSerializer(data.GetType());

        await using (var writer = new StreamWriter(targetFilePath))
        {
            xmlSerializer.Serialize(writer, data);

            writer.Close();
        }

        return;
    }
}
