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
            //String selectSQL = "SELECT TOP 5 * FROM topContributors2 ORDER BY Antal DESC;";
            String selectSQL = "SELECT TOP 5 ROW_NUMBER() OVER(ORDER BY Antal DESC) AS Placering, * FROM topContributors2;";

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
                    TopHaikuDetail.Th_Row = Convert.ToInt16(reader["Placering"]);

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

        public List<TopRatingDetail> SelectTopRatingList(out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            //String selectSQL = "SELECT TOP 5 * FROM topContributors2 ORDER BY Antal DESC;";
            String selectSQL = "SELECT TOP 5 ROW_NUMBER() OVER(ORDER BY Average DESC) AS Placering, * FROM showAverageRating;";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            SqlDataReader reader = null;

            List<TopRatingDetail> TopRatingList = new List<TopRatingDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    TopRatingDetail TopRatingDetail = new TopRatingDetail();
                    TopRatingDetail.Tr_Title = reader["Ha_Title"].ToString();
                    TopRatingDetail.Tr_Average = Convert.ToDouble(reader["Average"]);
                    TopRatingDetail.Tr_Votes = Convert.ToInt32(reader["NumberofVotes"]);
                    TopRatingDetail.Tr_Row = Convert.ToInt16(reader["Placering"]);

                    TopRatingList.Add(TopRatingDetail);

                }
                reader.Close();
                return TopRatingList;

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
