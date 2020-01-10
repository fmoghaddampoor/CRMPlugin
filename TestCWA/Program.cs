using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Workflow;
using Moq;
using Reply.Contoso.MDM.CWA;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Description;

namespace TestCWA
{
    class Program
    {
        static void Main(string[] args)
        {

            //string crmUri = ConfigurationManager.AppSettings["url"].ToString();    

            //string userName = ConfigurationManager.AppSettings["user"].ToString(); 

            //string password = ConfigurationManager.AppSettings["password"].ToString();

            ClientCredentials credentials = new ClientCredentials();

            var credential = new ClientCredentials();
            credential.UserName.UserName = ConfigurationManager.ConnectionStrings["user"].ToString();
            credential.UserName.Password = ConfigurationManager.ConnectionStrings["password"].ToString();
            var uri = new Uri(ConfigurationManager.ConnectionStrings["url"].ToString());

            var service = new OrganizationServiceProxy(uri, null, credential, null);


            service.EnableProxyTypes();
            var whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());

            var target = new MyCodeActivity(); //CWA che si vuole debuggare...deve essere referenziata nel progetto

            var serviceMock = new Mock<IOrganizationService>();
            var factoryMock = new Mock<IOrganizationServiceFactory>();
            var tracingServiceMock = new Mock<ITracingService>();
            var workflowContextMock = new Mock<IWorkflowContext>();
            var workflowUserId = Guid.NewGuid();
            var workflowCorrelationId = Guid.NewGuid();

            var workflowInitiatingUserId = whoAmIResponse.UserId;

            workflowContextMock.Setup(t => t.InitiatingUserId).Returns(workflowInitiatingUserId);
            workflowContextMock.Setup(t => t.CorrelationId).Returns(workflowCorrelationId);
            workflowContextMock.Setup(t => t.UserId).Returns(workflowUserId);

            var workflowContext = workflowContextMock.Object;
            //set up a mock tracingservice - will write output to console
            tracingServiceMock.Setup(t => t.Trace(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((t1, t2) => Console.WriteLine(t1, t2));
            var tracingService = tracingServiceMock.Object;
            factoryMock.Setup(t => t.CreateOrganizationService(It.IsAny<Guid?>())).Returns(service);

            var factory = factoryMock.Object;
            var invoker = new WorkflowInvoker(target);

            invoker.Extensions.Add<ITracingService>(() => tracingService);
            invoker.Extensions.Add<IWorkflowContext>(() => workflowContext);
            invoker.Extensions.Add<IOrganizationServiceFactory>(() => factory);


            //parametri in input della CWA -- la chiave deve avere lo stesso nome dell'input parameters
            var inputs = new Dictionary<string, object>
            {
                {
                    "Account", new EntityReference("account", new Guid("5AD25048-A021-EA11-A814-000D3A45B8A0"))
                }
            };
            var outputs = invoker.Invoke(inputs); //N.B. inserire un break point nella CWA per andare in debug
        }
    }
}
