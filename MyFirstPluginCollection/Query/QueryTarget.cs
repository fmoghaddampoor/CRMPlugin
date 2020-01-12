using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Reply.Contoso.Test.Contact.Plugins;
using System;
using System.Linq;

namespace MyFirstPluginCollection.Query
{
    /// <summary>
    /// Class for querying database implementing ITargetQueryable
    /// </summary>
    public class QueryTarget : ITargetQueryable
    {
        private PluginContext _pluginContext;
        private QueryExpression _queryAccess;
        /// <summary>
        /// Constructor for QueryTarget which takes context [Ex: Plugin context]
        /// </summary>
        /// <param name="context">
        /// context [Ex: Plugin context]
        /// </param>
        public QueryTarget(PluginContext context)
        {
            _pluginContext = context;
        }

        /// <summary>
        /// Search table by field logical value and field value
        /// </summary>
        /// <param name="tableName">
        /// Table name
        /// </param>
        /// <param name="fieldLogicalValue">
        /// Field logical value
        /// </param>
        /// <param name="fieldValue">
        /// Field value
        /// </param>
        /// <returns>
        /// Returns the entity that is found in search result
        /// </returns>
        public SearchTableResult SearchTable(string tableName, string fieldLogicalValue, string fieldValue)
        {
            _queryAccess = new QueryExpression(tableName);
            // Control field value is given
            _queryAccess.Criteria.AddCondition(fieldLogicalValue, ConditionOperator.Equal, fieldValue);
            // Do not lock the record so other users can access the searcched record
            _queryAccess.NoLock = true;
            // Return result including entity found
            return new SearchTableResult() {entity= _pluginContext.CrmServiceSystem.RetrieveMultiple(_queryAccess).Entities.FirstOrDefault() };
        }

        /// <summary>
        /// Update cell value by record id and field logical value
        /// </summary>
        /// <param name="tableName">
        /// Table name
        /// </param>
        /// <param name="RecordId">
        /// Record id
        /// </param>
        /// <param name="fieldLogicalValue">
        /// Field logical value
        /// </param>
        /// <param name="fieldValue">
        /// Field value
        /// </param>
        /// <returns>
        /// Returns the updated entity and if its updated
        /// </returns>
        public UpdateCellResult UpdateCell(string tableName, int RecordId, string fieldLogicalValue, string fieldValue)
        {
            var searchedEntity = _pluginContext.CrmServiceSystem.Retrieve(tableName, new Guid(RecordId.ToString()), new ColumnSet(new string[] {fieldLogicalValue}));
            return UpdateCell(searchedEntity, fieldLogicalValue, fieldValue);
        }

        /// <summary>
        /// Update cell by having record and field logical value
        /// </summary>
        /// <param name="record">
        /// record entity
        /// </param>
        /// <param name="fieldLogicalValue">
        /// Field logical value
        /// </param>
        /// <param name="fieldValue">
        /// Field value
        /// </param>
        /// <returns>
        /// Returns the updated entity and if its updated
        /// </returns>
        public UpdateCellResult UpdateCell(Entity record, string fieldLogicalValue, string fieldValue)
        {
            bool EntityFound = false;
            if (record != null)
            {
                // update field with new value
                record[fieldLogicalValue] = fieldValue;
                // submit change
                _pluginContext.CrmServiceSystem.Update(record);
                EntityFound = true;
            }
            return new UpdateCellResult() { entity = record, isUpdated = EntityFound };
        }

        /// <summary>
        /// Get post image entity
        /// </summary>
        /// <returns>
        /// Post image entity
        /// </returns>
        public EntityResult GetPostImage()
        {
            return new EntityResult() { entity = _pluginContext.GetPostImage() };
        }

        /// <summary>
        /// Get Pre image entity
        /// </summary>
        /// <returns>
        /// Pre image entity
        /// </returns>
        public EntityResult GetPreImage()
        {
            return new EntityResult() { entity = _pluginContext.GetPreImage() };
        }

        /// <summary>
        /// Get target entity (The entity which was target of user)
        /// </summary>
        /// <returns>
        /// target entity
        /// </returns>
        public EntityResult GetTarget()
        {
            return new EntityResult() { entity = _pluginContext.GetTarget() };
        }
    }
}
