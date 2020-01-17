using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Linq;
namespace CRMHandler
{
    public class CRMHandler
    {
        private readonly System.Uri _uri;
        private readonly ClientCredentials _clientCredentials;
        public CRMHandler(System.Uri uri, ClientCredentials clientCredentials)
        {
            _uri = uri;
            _clientCredentials = clientCredentials;
        }
        public List<string> GetAllEntityLogicalNames()
        {
            var organizationServiceProxy = new OrganizationServiceProxy(_uri, null, _clientCredentials, null);
            var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity | EntityFilters.Attributes };
            var organizationResponse = (RetrieveAllEntitiesResponse) organizationServiceProxy.Execute(request);
            List<string> lstEntityMetadata = new List<string>();
            foreach (var entityMetadata in organizationResponse.EntityMetadata)
            {
                lstEntityMetadata.Add(entityMetadata.LogicalName);
            }
            lstEntityMetadata.Sort();
            return lstEntityMetadata;
        }
        public SortedList<string, List<string>> GetAllEntities()
        {
            SortedList<string, List<string>> dicAllEntities = new SortedList<string, List<string>>();
            var organizationServiceProxy = new OrganizationServiceProxy(_uri, null, _clientCredentials, null);
            var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity | EntityFilters.Attributes };
            var organizationResponse = (RetrieveAllEntitiesResponse)organizationServiceProxy.Execute(request);
            foreach (var entityMetadata in organizationResponse.EntityMetadata)
            {
                dicAllEntities.Add(entityMetadata.LogicalName, entityMetadata.Attributes.OfType<string>().ToList());
            }
            return dicAllEntities;
        }
    }
}
