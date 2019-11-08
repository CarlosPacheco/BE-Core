using System.Data.Common;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// Extension Class to SqlCommand Object
    /// </summary>
    public static class SqlCommandExtensions
    {
        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query casted to a specified Type. 
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="command">The target <see cref="DbCommand"/>.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// (Nothing in Visual Basic) if the result set is empty. Returns a maximum of
        /// 2033 characters.
        /// </returns>
        public static TScalar ExecuteScalar<TScalar>(this DbCommand command)
        {
            return (TScalar)command.ExecuteScalar();
        }

        #region ExecuteScalar() : Shortcut/Type-direct extension methods

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query casted to an <see cref="int"/>. 
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="sqlCommand">The target <see cref="DbCommand"/>.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// (Nothing in Visual Basic) if the result set is empty. Returns a maximum of
        /// 2033 characters.
        /// </returns>
        public static int ExecuteScalarToInt(this DbCommand sqlCommand)
        {
            return sqlCommand.ExecuteScalar<int>();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query casted to a <see cref="long"/>. 
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="sqlCommand">The target <see cref="DbCommand"/>.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// (Nothing in Visual Basic) if the result set is empty. Returns a maximum of
        /// 2033 characters.
        /// </returns>
        public static long ExecuteScalarToLong(this DbCommand sqlCommand)
        {
            return sqlCommand.ExecuteScalar<long>();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query casted to a nullable<see cref="long"/>. 
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="sqlCommand">The target <see cref="DbCommand"/>.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// (Nothing in Visual Basic) if the result set is empty. Returns a maximum of
        /// 2033 characters.
        /// </returns>
        public static long? ExecuteScalarToNullableLong(this DbCommand sqlCommand)
        {
            return sqlCommand.ExecuteScalar<long?>();
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the
        /// result set returned by the query casted to a <see cref="string"/>. 
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="sqlCommand">The target <see cref="DbCommand"/>.</param>
        /// <returns>
        /// The first column of the first row in the result set, or a null reference
        /// (Nothing in Visual Basic) if the result set is empty. Returns a maximum of
        /// 2033 characters.
        /// </returns>
        public static string ExecuteScalarToString(this DbCommand sqlCommand)
        {
            return sqlCommand.ExecuteScalar<string>();
        }

        #endregion
    }
}
