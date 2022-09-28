using System.Data.SqlClient;

namespace HaikuLab3.Models
{
    public class TopHaikuMethods
    {
        public TopHaikuMethods() { }

        public List<TopHaikuDetail> SelectTopHaikuList(out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT TOP 5 * FROM topContributors2 ORDER BY Antal DESC;";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            SqlDataReader reader = null;

            List<TopHaikuDetail> TopHaikuList = new List<TopHaikuDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    TopHaikuDetail TopHaikuDetail = new TopHaikuDetail();
                    TopHaikuDetail.Th_Author = reader["Us_Alias"].ToString();
                    TopHaikuDetail.Th_Count = Convert.ToInt16(reader["Antal"]);

                    TopHaikuList.Add(TopHaikuDetail);

                }
                reader.Close();
                return TopHaikuList;

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
