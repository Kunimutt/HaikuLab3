using System.Data;
using System.Data.SqlClient;

namespace HaikuLab3.Models
{
    public class HaikuListMethods
    {
        public HaikuListMethods() { }


        public List<HaikuList> SelectHaikuList(out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT * FROM showAll";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            SqlDataReader reader = null;

            List<HaikuList> Haikulist = new List<HaikuList>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuList Haiku = new HaikuList();
                    Haiku.Hl_Title = reader["Ha_Title"].ToString();
                    Haiku.Hl_Author = reader["Us_Alias"].ToString();
                    Haiku.Hl_Genre = reader["Ge_Name"].ToString();

                    Haikulist.Add(Haiku);

                }
                reader.Close();
                return Haikulist;
               
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

        public List<HaikuList> FilterHaikuList(HaikuList haikulist, out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT * FROM showAll WHERE Ge_Name = @genre";

            

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("genre", SqlDbType.NVarChar, 70).Value = haikulist.Hl_Genre;
            SqlDataReader reader = null;

            List<HaikuList> Haikulist = new List<HaikuList>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuList Haiku = new HaikuList();
                    Haiku.Hl_Title = reader["Ha_Title"].ToString();
                    Haiku.Hl_Author = reader["Us_Alias"].ToString();
                    Haiku.Hl_Genre = reader["Ge_Name"].ToString();

                    Haikulist.Add(Haiku);

                }
                reader.Close();
                return Haikulist;

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
