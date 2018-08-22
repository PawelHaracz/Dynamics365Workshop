using System.Activities;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CRM.Workshops.Workflows
{
    /// <summary>
    /// Links:
    /// Process classes, attributes, and types: https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/workflow/process-classes-attributes-and-types
    /// </summary>
    public class SimpleWorkflow : CodeActivity
    {
        [RequiredArgument]
        [Input("Required Inpute")]
        public InArgument<string> RequiredInput { get; set; }

        [Input("Optional Input")]
        public InArgument<int> Input { get; set; }

        [Input("Optional Reference")]
        [ReferenceTarget("account")]    
        public InArgument<EntityReference> InputReference { get; set; }

        [Input("Int input")]
        [Output("Int output")]
        [Default("2322")]
        public InOutArgument<int> IntParameter { get; set; }

        [Output("Output")]
        public OutArgument<string> Output { get; set; }

        [Default("23.3")]
        public OutArgument<Money> MoneyOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var tracingService = context.GetExtension<ITracingService>();
            try
            {
                var workflowContext = context.GetExtension<IWorkflowContext>(); //workflow Context
                var serviceFactory = context.GetExtension<IOrganizationServiceFactory>(); //the same what's in SimplePlugin
                var service = serviceFactory.CreateOrganizationService(workflowContext.UserId); //the same what's in SimplePlugin

                //logic here          
                var input = Input.Get(context); //get value from input

                MoneyOutput.Set(context, new Money(input)); //set value to output
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