using System.Data;
using System.Data.SqlClient;

namespace HaikuLab3.Models
{
    public class RatingMethods
    {

        public RatingMethods() { }

        public int InsertRating(string id, string alias, int rating, out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "setRating2 @alias, @title, @rating;";

            // Lägg till en rating
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);

            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;
            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 30).Value = id;
            dbCommand.Parameters.Add("rating", SqlDbType.Int).Value = rating;

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Du har redan betygsatt denna haiku."; }
                return (i);
            }
            catch (Exception e)
            {

                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}
