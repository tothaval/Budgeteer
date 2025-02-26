namespace FileIO;

interface ISave
{

    public Task SaveAsJson(string targetFilePath);


    public Task SaveAsXml<T>(T data, string targetFilePath);

}
