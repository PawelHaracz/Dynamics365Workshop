using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CRM.Workshops.Examples
{
    public class FetchingData
    {
        private readonly IOrganizationService _organizationService;

        public FetchingData(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public IEnumerable<Entity> RetrieveMultipleByFetchXml()
        {
            var xml = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='account'>
    <attribute name='name' />
    <attribute name='address1_city' />
    <attribute name='primarycontactid' />
    <attribute name='telephone1' />
    <attribute name='accountid' />
    <order attribute='name' descending='false' />
    <filter type='and'>
      <condition attribute='ownerid' operator='eq-userid' />
      <condition attribute='statecode' operator='eq' value='0' />
    </filter>
    <link-entity name='contact' from='contactid' to='primarycontactid' visible='false' link-type='outer' alias='accountprimarycontactidcontactcontactid'>
      <attribute name='emailaddress1' />
    </link-entity>
  </entity>
</fetch>";
            var entityCollection = _organizationService.RetrieveMultiple(new FetchExpression(xml));

            var entities = entityCollection.Entities.ToArray();

            return entities;
        }

        public IEnumerable<Entity> RetrieveMultipleByQueryAttributes()
        {
            var queryAttribute = new QueryByAttribute("account");
            queryAttribute.AddAttributeValue(nameof(Account.Name).ToLower(), "Bury");
            queryAttribute.ColumnSet = new ColumnSet(nameof(Account.Name).ToLower(), "primarycontactid");
            queryAttribute.TopCount = 10;
            queryAttribute.Orders.Add(new OrderExpression("address1_city",OrderType.Ascending));

            var entityCollection = _organizationService.RetrieveMultiple(queryAttribute);
            var entities = entityCollection.Entities.ToArray();

            return entities;
        }

        public IEnumerable<Entity> RetrieveMultipleByQuerExpression()
        {
            var condition1 = new ConditionExpression("ownerid", ConditionOperator.EqualUserId);
            var condition2 = new ConditionExpression("statecode", ConditionOperator.Equal, 0);

            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition(condition1);
            filter.AddCondition(condition2);

            var link = new LinkEntity("contact", "account", "contactid", "primarycontactid", JoinOperator.Inner);
            link.Columns.AddColumn("emailaddress1");
           

            var queryExpression = new QueryExpression("account");
            queryExpression.Criteria.AddFilter(filter);
            queryExpression.ColumnSet = new ColumnSet(true);
            queryExpression.TopCount = 10;
            queryExpression.Orders.Add(new OrderExpression("address1_city", OrderType.Ascending));
            queryExpression.LinkEntities.Add(link);

            var entityCollection = _organizationService.RetrieveMultiple(queryExpression);
            var entities = entityCollection.Entities.ToArray();

            return entities;
        }

        public IEnumerable<Entity> GetAccountUsingLinqu()
        {
            using (var context = new XrmContext(_organizationService))
            {
                var accounts = context.AccountSet.Where(a => a.Name == "Bury").ToArray();

                return accounts;
            }
        }

        public Entity Retrive(Guid accountId)
        {
            var entity = _organizationService.Retrieve("account", accountId, new ColumnSet(false));

            return entity;
        }
    }
}
