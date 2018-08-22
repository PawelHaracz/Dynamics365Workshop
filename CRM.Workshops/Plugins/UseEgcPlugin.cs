using System;
using Microsoft.Xrm.Sdk;

namespace CRM.Workshops.Plugins
{
    public class UseEgcPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext));

            var account = ((Entity) context.InputParameters["Target"]).ToEntity<Account>();

            account.Address1_City = "Mielec";
        }
    }
}