using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;


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
        [Input("Account")]
        public InArgument<EntityReference> Account { get; set; }

        /// <summary>
        /// Output entity reference account parameter for already provided in a crm process
        /// </summary>
        [Output("NewAccount")]
        public OutArgument<EntityReference> NewAccount { get; set; }

        /// <summary>
        /// The custom workflow activity excecution method
        /// </summary>
        /// <param name="context">
        /// Activity context
        /// </param>
        protected override void Execute(CodeActivityContext context)
        {
           
        }
    }
}
