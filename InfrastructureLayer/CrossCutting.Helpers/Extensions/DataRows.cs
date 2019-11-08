using System;
using System.Data;

namespace CrossCutting.Helpers.Extensions
{
    public static class DataRows
    {
        public static T GetColumnValue<T>(this DataRow row, string columnName)
        {
            return row[columnName] == DBNull.Value ? default : (T)row[columnName];
        }
    }
}
