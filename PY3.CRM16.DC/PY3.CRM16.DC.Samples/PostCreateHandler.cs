using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PY3.CRM16.DC.Samples
{
    public partial class PostCreateHandler : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                // Defines the contextual information passed to a plug-in at run-time.
                // Contains information that describes the run-time environment that the plug-in is executing in,
                // information related to the execution pipeline, and entity business information.
                // https://msdn.microsoft.com/en-us/library/microsoft.xrm.sdk.ipluginexecutioncontext.aspx
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                // Represents a factory for creating IOrganizationService instances.
                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                // Provides programmatic access to the metadata and data for an organization.
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);

                if (!context.InputParameters.ContainsKey("Target") || !(context.InputParameters["Target"] is Entity)) return;

                var targetEntity = (Entity)context.InputParameters["Target"];

                var drink = service.Retrieve(targetEntity.LogicalName, targetEntity.Id, new ColumnSet(true));

                if (drink == null) return;

                var contactReference = drink.GetAttributeValue<EntityReference>("pub_contactid");

                var contact = service.Retrieve(contactReference.LogicalName, contactReference.Id, new ColumnSet(true));

                var contactDrinks = AllContactsDrinks(service, contact.Id);

                var totalUnits = TotalDrinksUnits(contactDrinks);

                Entity updatedContact = new Entity { LogicalName = "contact", Id = contact.Id };
                updatedContact.Attributes.Add("pub_thisyearsunittotal", totalUnits);
                service.Update(updatedContact);
            }

            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }

        EntityCollection AllContactsDrinks(IOrganizationService service, Guid contactId)
        {
            QueryExpression query = new QueryExpression { EntityName = "pub_drink", ColumnSet = new ColumnSet(true) };
            query.Criteria.AddCondition("pub_contactid", ConditionOperator.Equal, contactId);

            var allContactDrinks = service.RetrieveMultiple(query);

            return allContactDrinks;
        }

        int TotalDrinksUnits(EntityCollection drinks)
        {
            if (drinks == null || drinks.Entities == null || drinks.Entities.Count < 1) return 0;

            int units = 0;

            foreach (Entity drink in drinks.Entities)
            {
                if (drink.Attributes.Contains("pub_units"))
                    units += drink.GetAttributeValue<int>("pub_units");
            }

            return units;
        }
    }
}