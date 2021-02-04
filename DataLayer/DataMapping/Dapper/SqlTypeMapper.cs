using System;
using System.Reflection;
using Dapper;
using Data.Mapping.Dapper.Extensions;

namespace Data.Mapping.Dapper
{
    /// <summary>
    /// Provides type mapping setup and management
    /// </summary>
    public static class SqlTypeMapper
    {
        /// <summary>
        /// Name of the assembly containing the domain entities
        /// </summary>
        private const string DomainEntitiesAssembly = "Business";

        /// <summary>
        /// Indicates if domain-entities/POCOs mappings are already setted up.
        /// <remarks>Dapper specific</remarks>
        /// </summary>
        private static bool _domainEntitiesTypesMapped;

        /// <summary>
        /// Indicates if type handlers are already setted up
        /// </summary>
        /// <remarks>Dapper specific</remarks>
        private static bool _typeHandlersAdded;

        /// <summary>
        /// Sets all mappings and type handlers for Dapper and SqlServer interaction
        /// </summary>
        public static void SetupTypesMappingAndHandlers()
        {
            SetupDapperTypeHandlers();
            SetAllDomainEntitiesTypeMappings();
        }

        /// <summary>
        /// Adds needed type handlers on Dapper
        /// </summary>
        /// <remarks>Dapper specific</remarks>
        private static void SetupDapperTypeHandlers()
        {
            // Have type handlers been added already?
            if (_typeHandlersAdded)
            {
                return;
            }

            // Running multiple times won't register multiple instances, so we're safe on multiple call scenarios
            SqlMapper.AddTypeHandler(SqlGeographyHandler.Default);

            // Prevent setting type handlers again.
            _typeHandlersAdded = true;
        }

        /// <summary>
        /// Sets type maps for all types in a domain-entities/POCOs/VOs assembly.
        /// </summary>
        /// <remarks>Dapper specific. Assembly should only contain domain-entities/POCOs classes and nothing else</remarks>
        private static void SetAllDomainEntitiesTypeMappings()
        {
            // Have types been mapped already?
            if (_domainEntitiesTypesMapped)
            {
                return;
            }

            // Try to do all mappings, for all types defined in the domain-entities/POCOs/VOs assembly.
            try
            {
                Assembly.Load(DomainEntitiesAssembly).LoadSqlTypeMaps();

                // Prevent setting mappings again.
                _domainEntitiesTypesMapped = true;
            }
            catch (Exception ex)
            {
                // TODO: Logging! Log detailed, throw generic mapping error.
                throw new ApplicationException("Error while mapping Domain/Business entities properties (using ColumnMapping attribute) to DB column names (used with Dapper)", ex);
            }
        }

    }
}
