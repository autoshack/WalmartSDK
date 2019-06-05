using System.Collections.Generic;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Marketplace.Sample
{
    public class MockConfigurationProvider:IConfigurationProvider
    {
        private Dictionary<string, string> _configurations;

        public MockConfigurationProvider(Configuration config)
        {
            _configurations = new Dictionary<string, string>();

            Set("BaseUrl", config.BaseUrl);
            Set("ChannelType", config.ChannelType);
            Set("ServiceName", config.ServiceName);
            Set("StartupMessage", config.StartupMessage);
            Set("ClientId", config.Creds.ClientId);
            Set("ClientSecret", config.Creds.ClientSecret);
            Set("Logging", config.Logging.ToString());
            Set("Simulation", config.Simulation.ToString());


        }
        private string Get(string key)
        {
            return _configurations.ContainsKey(key) ? _configurations[key] : null;
        }

        private void Set(string key, string value)
        {
            _configurations[key] = value;
        }

        public string this[string key]
        {
            get { return this.Get(key); }
            set { this.Set(key,value); }
        }
    }
}