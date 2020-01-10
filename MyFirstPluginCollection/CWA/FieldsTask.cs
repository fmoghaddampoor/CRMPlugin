using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Xrm.Sdk;
using System.Net;
using Microsoft.Xrm.Sdk.Query;


namespace Reply.Sisal.MDM.CWA
{
    public class FieldsTask : CodeActivity
    {
        [Input("Task Configuration id")]
        [RequiredArgument]
        public InArgument<string> TaskConfigurationId { get; set; }
        [Output("Fields")]
        public OutArgument<string> Fields { get; set; }
        protected override void Execute(CodeActivityContext executionContext)
        {
            try
            {
                IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
                IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
                var org = serviceFactory.CreateOrganizationService(null);
                QueryExpression qe1 = new QueryExpression("sisal_configurationfield");
                var linkEntity1 = new LinkEntity("sisal_configurationfield", "sisal_sisal_taskconfiguration_sisal_configura", "sisal_configurationfieldid", "sisal_configurationfieldid", JoinOperator.Inner);
                var linkEntity2 = new LinkEntity("sisal_sisal_taskconfiguration_sisal_configura", "sisal_taskconfiguration", "sisal_taskconfigurationid", "sisal_taskconfigurationid", JoinOperator.Inner);
                linkEntity2.LinkCriteria = new FilterExpression();
                linkEntity2.LinkCriteria.AddCondition(new ConditionExpression("sisal_taskconfigurationid", ConditionOperator.Equal, TaskConfigurationId.Get(executionContext)));
                linkEntity1.LinkEntities.Add(linkEntity2);
                qe1.LinkEntities.Add(linkEntity1);
                qe1.ColumnSet = new ColumnSet("sisal_logicalnameontask", "sisal_requiredfield");
                qe1.NoLock = true;
                EntityCollection ec1 = org.RetrieveMultiple(qe1);
                var stringFields = "";
                foreach (var item in ec1.Entities)
                {
                    stringFields = stringFields + "," + item.GetAttributeValue<string>("sisal_logicalnameontask").ToString();
                    stringFields = stringFields + "," + item.GetAttributeValue<bool>("sisal_requiredfield").ToString();
                }
                Fields.Set(executionContext, stringFields);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"Error: {Environment.NewLine}{ex.Message}", ex);
            }      
        }
    }
}
