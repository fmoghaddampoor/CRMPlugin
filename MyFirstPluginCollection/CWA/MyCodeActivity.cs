using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;
using System;

namespace Reply.Contoso.MDM.CWA
{
    /// <summary>
    /// Activity class containing custom workflow action
    /// </summary>
    public class MyCodeActivity : CodeActivity
    {
        /// <summary>
        /// Input entity reference account parameter for already provided in a crm process
        /// </summary>
        [Input("Account")] // The one in action
        [ReferenceTarget("account")] // The entity logical name
        public InArgument<EntityReference> Account { get; set; }

        /// <summary>
        /// Output entity reference account parameter for already provided in a crm process
        /// </summary>
        [Output("NewAccount")] // The one in action
        [ReferenceTarget("account")] // The entity logical name
        public OutArgument<EntityReference> NewAccount { get; set; }

        /// <summary>
        /// The custom workflow activity excecution method
        /// </summary>
        /// <param name="context">
        /// Activity context
        /// </param>
        protected override void Execute(CodeActivityContext activityContext)
        {
            IWorkflowContext context = activityContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = activityContext.GetExtension<IOrganizationServiceFactory>();
            EntityReference accountEntityReference = Account.Get<EntityReference>(activityContext);
            //organization service has admin previledge
            var service = serviceFactory.CreateOrganizationService(null);
            Entity account = new Entity("account");
            account["name"] = "Learning crm";
            account["parentaccountid"] = accountEntityReference;
            var newAccountId = service.Create(account);
            NewAccount.Set(activityContext,new EntityReference("account", new Guid(newAccountId.ToString())));
        }
    }
}
