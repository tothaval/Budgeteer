namespace FileIO;

interface ILoad
{

    public Task<object> LoadJson();


    public Task<object> LoadXml(Type data, string targetFilePath);

}
