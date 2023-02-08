using log4net;
using sorec_gamma.modules.Config.Models;
using System;
using System.IO;
using System.Xml.Serialization;
using sorec_gamma.modules.ModuleMAJ;

namespace sorec_gamma.modules.Config
{
    public class ConfigUtils
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ConfigUtils));
        public const string dirName = "Config";
        public const string fileName = "Config.xml";
        public const string FileLoc = dirName + "\\" + fileName;
        private static readonly object _obj = new object();
        private static TerminalConfig _config = null;
        public static TerminalConfig ConfigData
        {
            get
            {
                if (_config == null)
                {
                    initTerminalConfigXmlFile();
                }

                return _config;
            }
        }
        public static void initTerminalConfigXmlFile()
        {
            if (!Directory.Exists(dirName))
            {
                _ = Directory.CreateDirectory(dirName);
            }
            if (!File.Exists(FileLoc))
            {
                TerminalConfig config = new TerminalConfig();
                if (createOrUpdateConfigFile(config))
                {
                    _config = config;
                }
            }
            else
            {
                _config = DeserializeTerminalConfigFile(FileLoc);
                if (_config == null)
                {
                    TerminalConfig config = new TerminalConfig();
                    if (createOrUpdateConfigFile(config))
                    {
                        _config = config;
                    }
                }
            }
        }
        public static bool createOrUpdateConfigFile(TerminalConfig config)
        {
            lock (_obj)
            {
                using (FileStream writter = new FileStream(FileLoc, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TerminalConfig));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(writter, config, ns);
                    return true;
                }
            }
        }
        private static TerminalConfig DeserializeTerminalConfigFile(string name)
        {
            TerminalConfig config = null;
            try
            {
                using (FileStream reader = File.OpenRead(name))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(TerminalConfig));
                    config = (TerminalConfig)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Deserialize journal file error: " + ex.Message + ex.StackTrace);
            }
            finally
            {
                if (config == null)
                {
                    _ = FileUtils.RenameOrMoveFile(name, dirName + "\\Config." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml.ERR");
                }
            }
            return config;
        }
    }
}
