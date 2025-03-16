using System.Xml.Serialization;

namespace FileIO
{
    public class Load : IOBase, ILoad
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // if needed, do not forget to update method parameters and the interface
        public Task<object> LoadJson()
        {
            throw new NotImplementedException();
        }


        public async Task<object> LoadXml<T>(string targetFilePath)
        {
            TargetFilePath = targetFilePath;


            XmlSerializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(TargetFilePath))
            {
                try
                {
                    Logger.Debug("loading {0} {1}", typeof(T), TargetFilePath);
                    var member = XmlSerializer.Deserialize(reader);

                    return member;
                }
                catch (Exception ex)
                {
                    Logger.Debug("Exception :{0}", ex.Message);

                }
                finally
                {
                    reader.Close();
                }

                return null;
            }
        }

    }
}
