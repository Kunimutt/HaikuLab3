using System.Data.SqlClient;
using System.Data;

namespace HaikuLab3.Models
{
    public class HaikuMethods
    {
        public HaikuMethods() { }


        public int InsertHaiku(HaikuDetail haiku, out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "INSERT INTO [Tbl_Haiku]  ([Ha_Author], [Ha_Genre], [Ha_Title], [Ha_Content], [Ha_Date]) SELECT [Us_Id], [Ge_Id], @title, @content, GETDATE() FROM [Tbl_User], [Tbl_Genre] WHERE [Us_Alias] = @author AND [Ge_Name] = @genre";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);

            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 70).Value = haiku.Ha_Title;
            dbCommand.Parameters.Add("content", SqlDbType.NVarChar, 70).Value = haiku.Ha_Content;
            dbCommand.Parameters.Add("author", SqlDbType.NVarChar, 30).Value = haiku.Ha_Alias;
            dbCommand.Parameters.Add("genre", SqlDbType.NVarChar, 70).Value = haiku.Ha_Genre;
            //dbCommand.Parameters.Add("photo", SqlDbType.Image).Value = haiku.Ha_Photo;


            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapades inte en user i databasen."; }
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

        public int UpdateUser7(UserDetail user, out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "UPDATE [Tbl_User] SET [Us_Email] = 'test@amail.com' WHERE [Us_Alias] = @alias";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);


            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = user.Us_Alias;


            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det uppdaterades inte i databasen."; }
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
