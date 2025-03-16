using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileIO
{
    public class IOBase
    {

        public XmlSerializer XmlSerializer { get; set; }
        public string TargetFilePath { get; set; } = string.Empty;

        public IOBase()
        {
            NLog.LogManager.Setup().LoadConfiguration(builder =>
            {
                builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "App_${shortdate}.txt");
            });
        }
    }
}
