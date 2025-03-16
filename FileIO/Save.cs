using NLog;
using System.Xml.Serialization;

namespace FileIO;

public class Save : IOBase, ISave
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    // if needed, do not forget to add the data object to the method parameters
    // and update the interface
    public Task SaveAsJson(string targetFilePath)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsXml<T>(T data, string targetFilePath)
    {
        if (data is null || string.IsNullOrWhiteSpace(targetFilePath))
            return;

        TargetFilePath = targetFilePath;

        XmlSerializer = new XmlSerializer(typeof(T));

        try
        {
            Logger.Debug("saving {0} {1}", typeof(T), TargetFilePath);

            await using (var writer = new StreamWriter(TargetFilePath))
            {
                XmlSerializer.Serialize(writer, data);

                writer.Close();
            }

        }
        catch (Exception ex)
        {
            Logger.Debug("Exception :{0}", ex.Message);
        }

        return;
    }
}
