using System.Configuration;

namespace Claro.Data.Configuration
{
    internal sealed class WebServiceConfigurationElement :
        ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("timeout", DefaultValue = "100000", IsRequired = false)]
        [IntegerValidator(ExcludeRange = true, MaxValue = 0)]
        public int Timeout
        {
            get { return (int)this["timeout"]; }
            set { this["timeout"] = value; }
        }
    }
}
