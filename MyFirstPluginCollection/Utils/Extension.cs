using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public static class GenericComparator
    {
        public static bool TryGetValue<T>(this Entity entity, string attribute, out T value)
        {
            value = default;

            if(entity.Contains(attribute) && entity.GetAttributeValue<T>(attribute) != null)
            {
                value = entity.GetAttributeValue<T>(attribute);
                return true;
            }

            return false;
        }

        public static bool TryGetValue<T>(this Entity entity, string attribute)
        {
            if (entity.Contains(attribute) && entity.GetAttributeValue<T>(attribute) != null)
            {
                return true;
            }

            return false;
        }

        public static bool Between(this DateTime value, DateTime min, DateTime max)
        {
            if (min.CompareTo(max) > 0)
            {
                return value.CompareTo(max) >= 0 && value.CompareTo(min) <= 0;
            }
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public static TProp Try<T, TProp>(this T source, Func<T, TProp> propertyAccessor, TProp defaultValue = default(TProp))
        {
            return Equals(source, default(T)) ? defaultValue : propertyAccessor(source);
        }


        /// <summary>
        /// Returns <c>True</c> if the current entity contains at least one of the specified fields,
        /// <c>False</c> otherwise.
        /// </summary>
        /// <param name="entity">The current entity.</param>
        /// <param name="fieldNames">The list of fields to check for.</param>
        /// <returns>
        /// <c>True</c> if the current entity contains at least one of the specified fields,
        /// <c>False</c> otherwise.
        /// </returns>
        public static bool ContainsAny(this Entity entity, IEnumerable<string> fieldNames)
        {
            return fieldNames.Any(entity.Contains);
        }

        /// <summary>
        /// Returns <c>True</c> if the current entity contains at least one of the specified fields,
        /// <c>False</c> otherwise.
        /// </summary>
        /// <param name="entity">The current entity.</param>
        /// <param name="fieldNames">The list of fields to check for.</param>
        /// <returns>
        /// <c>True</c> if the current entity contains at least one of the specified fields,
        /// <c>False</c> otherwise.
        /// </returns>
        public static bool ContainsAny(this Entity entity, params string[] fieldNames)
        {
            return fieldNames.Any(entity.Contains);
        }


        public static string Join(this IEnumerable<string> parts, string separator)
        {
            return string.Join(separator, parts);
        }

        public static Entity Retrieve(this IOrganizationService service, EntityReference target, ColumnSet columnSet)
        {
            return service.Retrieve(target.LogicalName, target.Id, columnSet);
        }

        public static Entity RetrieveWithNoLock(this IOrganizationService service, EntityReference entityReference, ColumnSet cols)
        {
            var getEntityMetadata = new RetrieveEntityRequest
            {
                LogicalName = entityReference.LogicalName,
                EntityFilters = EntityFilters.Attributes
            };

            var entityMetadata = (RetrieveEntityResponse)service.Execute(getEntityMetadata);
            var idOfEntity = entityMetadata.EntityMetadata.Attributes.Where(a => a.IsPrimaryId == true && a.IsValidForUpdate == false).First().SchemaName.ToLower();

            var myQueryExpression = new QueryExpression
            {
                ColumnSet = cols,
                EntityName = entityReference.LogicalName,
                NoLock = true,
                TopCount = 1,
                Criteria =
                {
                    Conditions = {
                        new ConditionExpression(idOfEntity, ConditionOperator.Equal, entityReference.Id)
                    }
                }
            };

            var entityList = service.RetrieveMultiple(myQueryExpression);

            return entityList.Entities.FirstOrDefault();
        }

        public static Entity RetrieveWithNoLock(this IOrganizationService service, string logicalName, Guid id, ColumnSet cols)
        {
            var er = new EntityReference(logicalName, id);
            return service.RetrieveWithNoLock(er, cols);
        }


        public static Entity Merge(this Entity main, Entity delta)
        {
            var entityName = main != null ? main.LogicalName : delta != null ? delta.LogicalName : string.Empty;
            var entityId = main != null ? main.Id : delta != null ? delta.Id : Guid.Empty;

            var entity = new Entity(entityName) { Id = entityId };

            if (main != null)
            {
                foreach (var attribute in main.Attributes)
                {
                    entity[attribute.Key] = attribute.Value;
                }
            }

            if (delta != null)
            {
                foreach (var attribute in delta.Attributes)
                {
                    entity[attribute.Key] = attribute.Value;
                }
            }

            return entity;
        }

        /// <summary>
        /// Creates an entity reference for the current entity
        /// </summary>
        /// <param name="e">The current entity</param>
        /// <returns>An entity reference pointing to the current entity</returns>
        public static EntityReference ToEntityReference(this Entity e)
        {
            return new EntityReference(e.LogicalName, e.Id);
        }

        public static EntityReference Clone(this EntityReference source)
        {
            return source == null ? null : new EntityReference(source.LogicalName, source.Id);
        }


        public static bool EqualsEntityReference(this EntityReference a, EntityReference b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;

            return a.Id == b.Id;
        }


        public static string GetLabelValue(this Label label, int languageCode)
        {
            var labelValue = label.LocalizedLabels.Where(x => x.LanguageCode == languageCode)
                .Select(x => x.Label)
                .FirstOrDefault();
            return string.IsNullOrEmpty(labelValue) ? string.Empty : labelValue;
        }

        public static List<int> GetValues(this Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata optionSetMetadata)
        {

            return
                (
                    from option in optionSetMetadata.Options
                    let value = option.Value
                    where value.HasValue
                    select value.Value
                )
                    .ToList();
        }

        public static bool IsAttributeEquals(this Entity entity1, string entity1Field, Entity entity2, string entity2Field)
        {

            //if(!entity1.Contains(entity1Field) && !entity2.Contains(entity2Field))
            //	return true;


            //if (!entity1.Contains(entity1Field) && entity2.Contains(entity2Field))
            //	return false;

            if (!entity1.Contains(entity1Field))
                return !entity2.Contains(entity2Field);

            if (entity1.Contains(entity1Field) && !entity2.Contains(entity2Field))
                return false;


            var entity1FieldValue = entity1[entity1Field];
            var entity2FieldValue = entity2[entity2Field];

            //il campo è stato svuotato
            if (entity1FieldValue == null)
            {
                return entity2FieldValue == null;
            }

            // entity1FieldValue != null
            if (entity2FieldValue == null)
            {
                return false;
            }



            if (entity1FieldValue is OptionSetValue)
            {
                var targetOptionSetValue = ((OptionSetValue)entity2FieldValue).Value;
                var sourceOptionSetValue = ((OptionSetValue)entity1FieldValue).Value;

                return targetOptionSetValue.Equals(sourceOptionSetValue);
            }

            if (entity1FieldValue is EntityReference)
            {
                var targetId = ((EntityReference)entity2FieldValue).Id;
                var sourceId = ((EntityReference)entity1FieldValue).Id;

                return targetId.Equals(sourceId);
            }

            return entity1FieldValue.Equals(entity2FieldValue);

        }

        public static bool IsAttributeEquals(this Entity entity1, string entity1Field, object entity2FieldValue)
        {
            if (!entity1.Contains(entity1Field)) return false;

            var entity1FieldValue = entity1[entity1Field];

            //il campo è stato svuotato
            if (entity1FieldValue == null) return entity2FieldValue == null;

            //valore passato null ma campo della entity1 valorizzato
            if (entity2FieldValue == null) return false;

            if (entity1FieldValue is OptionSetValue)
            {
                var targetOptionSetValue = ((OptionSetValue)entity2FieldValue).Value;
                var sourceOptionSetValue = ((OptionSetValue)entity1FieldValue).Value;

                return targetOptionSetValue.Equals(sourceOptionSetValue);
            }

            if (entity1FieldValue is EntityReference)
            {
                var targetId = ((EntityReference)entity2FieldValue).Id;
                var sourceId = ((EntityReference)entity1FieldValue).Id;

                return targetId.Equals(sourceId);
            }

            return entity1FieldValue.Equals(entity2FieldValue);
        }


        public static bool IsEquals(this EntityReference main, EntityReference other)
        {
            if (ReferenceEquals(main, other)) return true;

            if (ReferenceEquals(other, null) || ReferenceEquals(main, null)) return false;

            return main.Id == other.Id && main.LogicalName == other.LogicalName;

        }

        public static T GetValue<T>(this Entity entity, string attributeName)
        {
            return entity.GetAttributeValue<T>(attributeName);
        }

        public static void SetValue<T>(this Entity entity, string attributeName, T value)
        {
            entity[attributeName] = value;
        }
    }
}
