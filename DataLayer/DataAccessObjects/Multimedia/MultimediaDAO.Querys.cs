
using Dapper;

namespace Data.AccessObjects.MultimediaFiles
{
    public partial class MultimediaDao
    {
        /// <summary>
        /// Sql query template for Get.
        /// Dapper SqlBuilder placeholders included in, if needed
        /// </summary>
        private const string QueryGetBody = @"SELECT * FROM ... /**where**/";


        /// <summary>
        /// Sql query template for GetByIdentifier method.
        /// Dapper SqlBuilder placeholders included in.
        /// </summary>
        private const string QueryGetByIdentifier = @"/* Content QueryGetByIdentifier */
           SELECT *
	        FROM 
	        WHERE Id = :id";

        private const string QueryUpdate = @"UPDATE ... SET ... WHERE ...";

        private const string QueryCreate = @"
        INSERT INTO 
	        (

	        )
	        VALUES 
	        (

	        );

	        SELECT CAST(SCOPE_IDENTITY() as INT);
        ";

        private const string QueryDelete = @"DELETE FROM WHERE Id = :Id";
    }
}