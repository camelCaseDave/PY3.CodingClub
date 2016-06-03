using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace PY3.CRM16.DC.Samples
{
    public partial class TimeSinceLastDrink : CodeActivity
    {
        [RequiredArgument]
        [Input("Latest drink")]
        [ReferenceTarget("pub_drink")]
        public InArgument<EntityReference> LatestDrink { get; set; }

        [Output("DaysBetweenDrinks")]
        public OutArgument<double> DaysBetweenDrinks { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            var latestDrinkReference = LatestDrink.Get(executionContext);

            var latestDrink = service.Retrieve(latestDrinkReference.LogicalName, latestDrinkReference.Id, new ColumnSet(true));

            var contactReference = latestDrink.GetAttributeValue<EntityReference>("pub_contactid");

            var contactDrinks = ContactDrinks(service, contactReference.Id, latestDrink.Id);

            var lastDrink = ContactPreviousDrink(contactDrinks);

            if (lastDrink == null || latestDrink == null) return;

            var lastDrinkDate = lastDrink.GetAttributeValue<DateTime>("createdon");

            var latestDrinkDate = latestDrink.GetAttributeValue<DateTime>("createdon");

            var daysBetweenDrinks = (latestDrinkDate - lastDrinkDate).TotalDays;

            DaysBetweenDrinks.Set(executionContext, daysBetweenDrinks);
        }

        EntityCollection ContactDrinks(IOrganizationService service, Guid contactId, Guid latestDrinkId)
        {
            QueryExpression query = new QueryExpression { EntityName = "pub_drink", ColumnSet = new ColumnSet(true) };
            query.Criteria.AddCondition("pub_contactid", ConditionOperator.Equal, contactId);
            query.Criteria.AddCondition("pub_drinkid", ConditionOperator.NotEqual, latestDrinkId);
            query.AddOrder("createdon", OrderType.Descending);

            var contactDrinks = service.RetrieveMultiple(query);

            return contactDrinks;
        }

        Entity ContactPreviousDrink(EntityCollection contactDrinks)
        {
            return contactDrinks != null && contactDrinks.Entities != null && contactDrinks.Entities.Count > 0
                ? contactDrinks[0] : null;
        }
    }
}
