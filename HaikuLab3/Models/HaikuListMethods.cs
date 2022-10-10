using System.Data;
using System.Data.SqlClient;

namespace HaikuLab3.Models
{
    public class HaikuListMethods
    {
        public HaikuListMethods() { }


        public List<HaikuListDetail> SelectHaikuList(out string errormsg)
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

            List<HaikuListDetail> Haikulist = new List<HaikuListDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuListDetail Haiku = new HaikuListDetail();
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

        public List<HaikuListDetail> SelectHaikuList(out string errormsg,string filter)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT * FROM showAll WHERE Ge_Name = @filter";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("filter", SqlDbType.NVarChar, 30).Value = filter;
           // dbCommand.Parameters.Add("sortera", SqlDbType.NVarChar, 30).Value = sortera;
            SqlDataReader reader = null;

            List<HaikuListDetail> Haikulist = new List<HaikuListDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuListDetail Haiku = new HaikuListDetail();
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

        public List<HaikuListDetail> SelectHaikuListForUser(out string errormsg, string alias)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT Ha_Title, Ge_name FROM Tbl_Haiku, Tbl_Genre, Tbl_User WHERE Tbl_User.Us_Id = Tbl_Haiku.Ha_Author AND Tbl_Haiku.Ha_Genre = Tbl_Genre.Ge_Id AND Us_Alias = @alias;";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;
            // dbCommand.Parameters.Add("sortera", SqlDbType.NVarChar, 30).Value = sortera;
            SqlDataReader reader = null;

            List<HaikuListDetail> Haikulist = new List<HaikuListDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuListDetail Haiku = new HaikuListDetail();
                    Haiku.Hl_Title = reader["Ha_Title"].ToString();
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

        public List<HaikuListDetail> SearchHaikuList(out string errormsg, string search)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT * FROM showAll WHERE Ha_Title LIKE '%' + @search + '%' OR Us_Alias LIKE '%' + @search + '%' OR Ge_Name LIKE '%' + @search + '%'";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("search", SqlDbType.NVarChar, 30).Value = search;

            SqlDataReader reader = null;

            List<HaikuListDetail> Haikulist = new List<HaikuListDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuListDetail Haiku = new HaikuListDetail();
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
