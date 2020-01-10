using Microsoft.Xrm.Sdk;
using Reply.Contoso.Test.Contact.Plugins;
using System;

namespace MyFirstPluginCollection
{
    public class OnNewAccountCreatedAsscociateMasterAccount : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = new PluginContext(serviceProvider);
            var targetEntity = pluginContext.GetTarget();
            Query.QueryTarget queryTarget = new Query.QueryTarget(pluginContext);
            var preImage = queryTarget.GetPreImage();
        }
    }
}
