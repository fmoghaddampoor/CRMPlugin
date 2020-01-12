using Microsoft.Xrm.Sdk;
using Reply.Contoso.Test.Contact.Plugins;
using System;

namespace MyFirstPluginCollection
{
    public class OnNewAccountCreatedAsscociateMasterAccount : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            Query.QueryTarget queryTarget = new Query.QueryTarget(serviceProvider);
            var preImage = queryTarget.GetPreImage();
        }
    }
}
