using System;
using Microsoft.Xrm.Sdk;

namespace CRM.Workshops.Plugins
{
    public class PluginWithConfiguration : IPlugin
    {
        private readonly string _unsecurityConfig;
        private readonly string _secureConfig;

        public PluginWithConfiguration(string unsecurityConfig, string secureConfig)
        {
            _unsecurityConfig = unsecurityConfig; //passed configuration what is encrypted in CRM
            _secureConfig = secureConfig; //passed configuration what is crypted in CRM
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException(); //the same logic what in SimplePlugin
        }
    }
}