using System.Configuration;
using ResumeParser.Model;

namespace ResumeParser
{
    internal class ConfigFileApplicationSettingsAdapter : IApplicationSettings
    {
        public string InputReaderLocation
        {
            get { return ConfigurationManager.AppSettings["InputReaderLocation"]; }
        }
    }
}
