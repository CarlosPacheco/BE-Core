using System;
using System.Collections.Generic;
using System.Text;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Types;

namespace Data.Mapping.Dapper.Oracle
{
    public static class OracleUdt
    {
        public static string ConvertToUdt<T>(string sql, List<T> list, string udtTableName, Func<T, string> convertAction, string parameterName = "p0")
        {
            //var query = new StringBuilder("(");

            string className = typeof(T).Name.ToUpperInvariant();

            string listQuery = string.Empty;

            foreach (T elem in list)
            {
                listQuery += $@"{parameterName}.extend; {parameterName}({parameterName}.last) := {className}({convertAction(elem)});
";
            }

            string declares = $@"declare
            {parameterName} {udtTableName} := {udtTableName}();
                
            BEGIN
            
            {listQuery}

            {sql}
            END;";
            return declares;
        }
    }

    //public interface INullableOracleCustomType: INullable,  IOracleCustomType
    //{
    //}


    //public abstract class CustomTypeBase<T> : 
    //    IOracleCustomType, IOracleCustomTypeFactory, INullable where T : CustomTypeBase<T>, new()
    //{
    //    private bool _isNull;
    //    public bool IsNull
    //    {
    //        get { return this._isNull; }
    //    }
    //    [OracleObjectMapping("ARTICLE_ID")]
    //    public static T Null
    //    {
    //        get { return new T { _isNull = true }; }
    //    }

    //    public IOracleCustomType CreateObject()
    //    {
    //        return new T();
    //    }

    //    public abstract void FromCustomObject(OracleConnection con, IntPtr pUdt);
    //    public abstract void ToCustomObject(OracleConnection con, IntPtr pUdt);        
    //}
}
