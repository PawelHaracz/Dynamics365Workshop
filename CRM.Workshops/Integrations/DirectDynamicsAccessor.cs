using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;

namespace CRM.Workshops.Integrations
{
    public class DirectDynamicsAccessor
    {
        private readonly OrganizationWebProxyClient _sdk;
        private readonly CrmServiceClient _crmServiceClient;
        public DirectDynamicsAccessor(CrmServiceClient crmServiceClient)
        {
            _crmServiceClient = crmServiceClient;
            _sdk = crmServiceClient.OrganizationWebProxyClient;
        }

        public DirectDynamicsAccessor(string login, string password, string organizatioName)
        {
            _crmServiceClient = new CrmServiceClient(login, CrmServiceClient.MakeSecureString(password), string.Empty, organizatioName);
            _sdk = _crmServiceClient.OrganizationWebProxyClient;
        }

        public T ExecuteCrm<T>(Func<OrganizationWebProxyClient, T> func)
        {
            return func(_sdk);
        }
    }
}
