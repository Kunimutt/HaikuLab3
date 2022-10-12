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
            String insertSQL = "insertHaikuControll3 @title, @content, @author, @genre";

            //String insertSQL = "INSERT INTO [Tbl_Haiku]  ([Ha_Author], [Ha_Genre], [Ha_Title], [Ha_Content], [Ha_Date]) SELECT [Us_Id], [Ge_Id], @title, @content, GETDATE() FROM [Tbl_User], [Tbl_Genre] WHERE [Us_Alias] = @author AND [Ge_Name] = @genre";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);

            string allhaiku = haiku.Ha_Content1 + haiku.Ha_Content2 + haiku.Ha_Content3;

            dbCommand.Parameters.Add("title", SqlDbType.NVarChar, 70).Value = haiku.Ha_Title;
            dbCommand.Parameters.Add("content", SqlDbType.NVarChar, 135).Value = allhaiku;
            dbCommand.Parameters.Add("author", SqlDbType.NVarChar, 30).Value = haiku.Ha_Alias;
            dbCommand.Parameters.Add("genre", SqlDbType.NVarChar, 70).Value = haiku.Ha_Genre;
            //dbCommand.Parameters.Add("photo", SqlDbType.Image).Value = haiku.Ha_Photo;

            //try
            //{
            //    dbConnection.Open();
            //    int haikuCount = (int)dbCommand.ExecuteScalar();
            //    if (haikuCount == 1)
            //    {
            //        errormsg = "";
            //    }
            //    else { errormsg = "Ange korrekt alias och email eller skapa ett nytt konto"; }
            //    return (haikuCount);
            //}
            //catch (Exception e)
            //{
            //    errormsg = e.Message;
            //    return 0;
            //}
            //finally
            //{
            //    dbConnection.Close();
            //}

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Haikun kunde inte läggas till i databasen."; }
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

        
        public List<HaikuDetail>  SelectHaiku(out string errormsg, string id)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();



            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True"; // <- gå in på properties på databasen, under connection string



            // SQL-sträng
            String selectSQL = "SELECT * FROM showHaiku WHERE Ha_Title = @id";



            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("id", SqlDbType.NVarChar, 30).Value = id;



            SqlDataReader reader = null;

            List<HaikuDetail> Haikulist = new List<HaikuDetail>();


            //HaikuDetail Haikudetalj = new HaikuDetail();
            errormsg = "";



            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    HaikuDetail Haiku = new HaikuDetail();
                    Haiku.Ha_Title = reader["Ha_Title"].ToString();
                    Haiku.Ha_Content = reader["Ha_Content"].ToString();
                    Haiku.Ha_Alias = reader["Us_Alias"].ToString();
                    Haiku.Ha_Genre = reader["Ge_Name"].ToString();

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

        public List<RatingDetail> SelectRating(out string errormsg2, string id)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT Average FROM showAverageRating WHERE Ha_Title = @id"; 



            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("id", SqlDbType.NVarChar, 30).Value = id;



            SqlDataReader reader = null;
            List<RatingDetail>Ratinglist = new List<RatingDetail>();


            //RatingDetail ratingDetail = new RatingDetail();
            errormsg2 = "";



            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    RatingDetail rating = new RatingDetail();
                    //rating.Ra_RatingAverage = Convert.ToInt16(reader["Average"]);
                    rating.Ra_RatingAverage = Convert.ToDouble(reader["Average"]);
                    
                    Ratinglist.Add(rating);
                    //ratingDetail = rating;

                }
                reader.Close();
                return Ratinglist;

            }
            catch (Exception e)
            {
                errormsg2 = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
