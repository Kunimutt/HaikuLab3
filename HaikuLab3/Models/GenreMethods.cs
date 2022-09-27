using System.Data.SqlClient;

namespace HaikuLab3.Models
{
    public class GenreMethods
    {
        public GenreMethods() { }

        public List<GenreDetail> SelectGenreList(out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT [Ge_Name] FROM [Tbl_Genre]";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            SqlDataReader reader = null;

            List<GenreDetail> GenreList = new List<GenreDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    GenreDetail Genre = new GenreDetail();
                    Genre.Ge_Name = reader["Ge_Name"].ToString();

                    GenreList.Add(Genre);

                }
                reader.Close();
                return GenreList;

            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }


    }
}

