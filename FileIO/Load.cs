using System.Xml.Serialization;

namespace FileIO
{
    public class Load : ILoad
    {
        // if needed, do not forget to update method parameters and the interface
        public Task<object> LoadJson()
        {
            throw new NotImplementedException();
        }


        public async Task<object> LoadXml(Type data, string targetFilePath)
        {
            var xmlSerializer = new XmlSerializer(data);


            using (var reader = new StreamReader(targetFilePath))
            {
                try
                {
                    var member = xmlSerializer.Deserialize(reader);

                    return member;
                }
                catch (Exception)
                {

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
