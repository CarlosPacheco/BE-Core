using System;

namespace Data.Mapping.SqlTypes
{
    public static class Udtt
    {
        /// <summary>
        /// Enum for generic DB User Defined Table Types created.
        /// Intended to be used with ToString method to pass the type name
        /// to SQL Server.
        /// Enumeration elements must have the exact (case-sensitive) name
        /// as the UDT Type in the DB.
        /// </summary>
        public enum UserDefinedTableTypes
        {
            GenericTableForIdentifier,
            GenericTableForIdentifierTuple,
            GenericTableForIdentifierAsString,
            GenericTableForIdentifiedKeyValuePairs,
            GenericTableForKeyValuePairs,
            GenericTableForStringTuple,
            GenericTableForStringTriple,
            TableWithVarcharIds,
            GenericTableForTranslationCodes,
            GenericTableForDecimal
        }
    }
}
