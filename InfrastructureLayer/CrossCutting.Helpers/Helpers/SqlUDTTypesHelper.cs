using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrossCutting.Helpers.Helpers
{
    //TODO:: better solution -> check this Github: https://github.com/RasicN/Dapper-Parameters 
    //https://stackoverflow.com/questions/56190787/can-dapper-pass-a-user-defined-composite-type-to-a-postgresql-function
    public static class SqlUdtTypesHelper
    {
        /// <summary>
        /// Enumeration that represents column names, each element's name must exactly the one expected by SQL procedures.
        /// </summary>
        private enum KeyValuePairTableColumnName
        {
            Id,
            Key,
            Value
        }

        /// <summary>
        /// Enumeration that represents column names, each element's name must exactly the one expected by SQL procedures.
        /// </summary>
        private enum StringTupleTableColumnName
        {
            Id,
            T1,
            T2,
            T3
        }

        public static DataTable CreateIdValuePairTable<TK, TV>(Dictionary<TK, TV> keyValuePairs)
        {
            DataTable table = new DataTable();

            table.Columns.Add(KeyValuePairTableColumnName.Id.ToString(), typeof(TK));
            table.Columns.Add(KeyValuePairTableColumnName.Value.ToString(), typeof(TV));

            if (keyValuePairs == null)
            {
                return table;
            }

            foreach (KeyValuePair<TK, TV> kvp in keyValuePairs)
            {
                table.Rows.Add(kvp.Key, kvp.Value);
            }

            return table;
        }

        public static DataTable CreateIdValuePairTable<TK, TV>(List<Tuple<TK, TV>> keyValuePairs)
        {
            DataTable table = new DataTable();

            table.Columns.Add(KeyValuePairTableColumnName.Id.ToString(), typeof(TK));
            table.Columns.Add(KeyValuePairTableColumnName.Value.ToString(), typeof(TV));

            if (keyValuePairs == null)
            {
                return table;
            }

            foreach (Tuple<TK, TV> kvp in keyValuePairs)
            {
                table.Rows.Add(kvp.Item1, kvp.Item2);
            }

            return table;
        }


        public static DataTable CreateIdColumnTable<T>(IEnumerable<T> values)
        {
            DataTable table = new DataTable();

            table.Columns.Add(KeyValuePairTableColumnName.Id.ToString(), typeof(T).IsSubclassOf(typeof(Enum)) ? typeof(int) : typeof(T));

            if (values == null)
            {
                return table;
            }

            foreach (T value in values)
            {
                table.Rows.Add(value);
            }

            return table;
        }

        public static DataTable CreateIdentifiedKeyValuePairsTable<T, TK, TV>(List<Tuple<T, TK, TV>> identifiedKeyValuePairs)
        {
            DataTable table = new DataTable();

            table.Columns.Add(KeyValuePairTableColumnName.Id.ToString(), typeof(T));
            table.Columns.Add(KeyValuePairTableColumnName.Key.ToString(), typeof(TK));
            table.Columns.Add(KeyValuePairTableColumnName.Value.ToString(), typeof(TV));

            if (identifiedKeyValuePairs == null)
            {
                return table;
            }

            foreach (Tuple<T, TK, TV> tp in identifiedKeyValuePairs)
            {
                table.Rows.Add(tp.Item1, tp.Item2, tp.Item3);
            }

            return table;
        }

        public static DataTable CreateStringTupleTable<T, TK, TV>(List<Tuple<T, TK, TV>> stringTriple)
        {
            DataTable table = new DataTable();

            table.Columns.Add(StringTupleTableColumnName.Id.ToString(), typeof(T));
            table.Columns.Add(StringTupleTableColumnName.T1.ToString(), typeof(TK));
            table.Columns.Add(StringTupleTableColumnName.T2.ToString(), typeof(TV));

            if (stringTriple == null)
            {
                return table;
            }

            foreach (Tuple<T, TK, TV> tp in stringTriple)
            {
                table.Rows.Add(tp.Item1, tp.Item2, tp.Item3);
            }

            return table;
        }

        public static DataTable CreateStringTupleTable(Dictionary<string, string> stringTuples)
        {
            List<Tuple<long, string, string>> tupleList = stringTuples.Select(t => new Tuple<long, string, string>(0, t.Key, t.Value)).ToList();

            return CreateStringTupleTable(tupleList);
        }

        public static DataTable CreateStringTripleTable<T, TK, TV, TB>(List<Tuple<T, TK, TV, TB>> stringTriple)
        {
            DataTable table = new DataTable();

            table.Columns.Add(StringTupleTableColumnName.Id.ToString(), typeof(T));
            table.Columns.Add(StringTupleTableColumnName.T1.ToString(), typeof(TK));
            table.Columns.Add(StringTupleTableColumnName.T2.ToString(), typeof(TV));
            table.Columns.Add(StringTupleTableColumnName.T3.ToString(), typeof(TB));

            if (stringTriple == null)
            {
                return table;
            }

            foreach (Tuple<T, TK, TV, TB> tp in stringTriple)
            {
                table.Rows.Add(tp.Item1, tp.Item2, tp.Item3, tp.Item4);
            }

            return table;
        }
    }
}
