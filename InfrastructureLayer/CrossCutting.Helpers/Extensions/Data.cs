using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using CrossCutting.Helpers.Conversions.Attributes;

namespace CrossCutting.Helpers.Extensions
{
    public static class Data
    {
        public static DataTable ToDataTable(this DbCommand command)
        {
            using (IDataReader dataReader = command.ExecuteReader())
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                return dataTable;
            }
        }

        /// <summary>
        /// Create DataAdapter a fill the result to a DataSet
        /// </summary>
        /// <param name="command"></param>
        /// <returns>DataSet with the query result</returns>
	    public static DataSet ToDataSet(this DbCommand command)
        {
            using (DbDataAdapter dataAdapter = new SqlDataAdapter())
            using (DataSet dataSet = new DataSet())
            {
                if (dataAdapter != null)
                {
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataSet);
                }

                return dataSet;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            if (data == null)
            {
                return null;
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            foreach (PropertyDescriptor propertyDescriptor in properties)
            {
                dataTable.Columns.Add(propertyDescriptor.Name, Nullable.GetUnderlyingType(propertyDescriptor.PropertyType) ?? propertyDescriptor.PropertyType);
            }
            foreach (T current in data)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyDescriptor propertyDescriptor in properties)
                {
                    dataRow[propertyDescriptor.Name] = (propertyDescriptor.GetValue(current) ?? DBNull.Value);
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public static DataTable ToFilteredDataTable<T>(this IList<T> data)
        {
            Type typeFromHandle = typeof(T);
            DataTable dataTable = new DataTable();
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                DataTableConverter dataTableConverter = (DataTableConverter)Attribute.GetCustomAttribute(propertyInfo, typeof(DataTableConverter));
                if (dataTableConverter == null || dataTableConverter.ShouldConvert)
                {
                    dataTable.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }
            }
            foreach (T current in data)
            {
                DataRow dataRow = dataTable.NewRow();
                array = properties;
                foreach (var propertyInfo in array)
                {
                    DataTableConverter dataTableConverter = (DataTableConverter)Attribute.GetCustomAttribute(propertyInfo, typeof(DataTableConverter));
                    if (dataTableConverter == null || dataTableConverter.ShouldConvert)
                    {
                        dataRow[propertyInfo.Name] = (propertyInfo.GetValue(current) ?? DBNull.Value);
                    }
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
    }
}
