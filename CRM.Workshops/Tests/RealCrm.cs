using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace CRM.Workshops.Tests
{
    [TestClass]
    public class RealCrm
    {
        private readonly XrmFakedContext _fakedContextctx;
        public RealCrm()
        {
            _fakedContextctx = new XrmRealContext(new CrmServiceClient("admin@m365x032832.onmicrosoft.com", CrmServiceClient.MakeSecureString("pawel.harac@5471"), "EMEA", "m365x032832"));
            
            
        }
    }
}
