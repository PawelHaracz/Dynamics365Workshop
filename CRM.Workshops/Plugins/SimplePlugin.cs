using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace CRM.Workshops.Plugins
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/gg328576.aspx -> list all supported messages
    /// https://msdn.microsoft.com/en-us/library/gg328576.aspx -> sdk
    /// </summary>
    public class SimplePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) // serviceProvider -> Factory of all CRM's service (some kind of DI)
        {
            var tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));  //ITracingService -> Logger
            try
            {
                tracingService.Trace("Starting...");
                var context = (IPluginExecutionContext) serviceProvider.GetService(typeof(IPluginExecutionContext)); //IPluginExecutionContext -> crm Context, inlude meta and record inforamtion
                var serviceFactory =
                    (IOrganizationServiceFactory) serviceProvider.GetService(typeof(IOrganizationServiceFactory)); //Factory to generate OrganizationService. 
                var service = serviceFactory.CreateOrganizationService(context.UserId); //ServiceOrganization -> Service responsible for invoking action in a crm. Passed CRM System User Id (Guid), on which credential will work
                var entity = (Entity) context.InputParameters["Target"]; // in Plugin contains target entity as Input Parameters. Contains only what was changed (not inlclude all events, e.g setState, assosiate)
                tracingService.Trace("Initialized all necessary object ");

                //logic here            
                tracingService.Trace("");

            }
            catch (FaultException<OrganizationServiceFault> ex) //handled only wcf errors emitted from IOrganizationService
            {
                tracingService.Trace($"Error: {ex.Message}");
                throw new InvalidPluginExecutionException();
            }
            finally
            {
                tracingService.Trace("Finished");
            }
        }
    }
}
