using System;
using Microsoft.Xrm.Sdk;

namespace CRM.Workshops.Examples
{
    public class Crud
    {
        private readonly IOrganizationService _organizationService;

        public Crud(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public Guid Create(Entity entity)
        {
            var id = _organizationService.Create(entity);

            return id;
        }

        public void Update(Entity entity)
        {
            _organizationService.Update(entity);
        }


        public void Delete(EntityReference entityReference)
        {
            _organizationService.Delete(entityReference.LogicalName, entityReference.Id);
        }
    }
}