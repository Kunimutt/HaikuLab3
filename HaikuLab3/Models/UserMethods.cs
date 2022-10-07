using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace HaikuLab3.Models
{
    public class UserMethods
    {
        public UserMethods() { }



        public int InsertUser(UserDetail user, out string errormsg)
        {
            
            

            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "INSERT INTO [Tbl_User] ([Us_Fname], [Us_Lname], [Us_Alias], [Us_Age], [Us_Email]) values (@fname, @lname, @alias, @age, @email)";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);

            dbCommand.Parameters.Add("fname", SqlDbType.NVarChar, 30).Value = user.Us_Fname;
            dbCommand.Parameters.Add("lname", SqlDbType.NVarChar, 30).Value = user.Us_Lname;
            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = user.Us_Alias;
            dbCommand.Parameters.Add("age", SqlDbType.Int).Value = user.Us_Age;
            dbCommand.Parameters.Add("email", SqlDbType.NVarChar, 50).Value = user.Us_Email;
            
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

        public int UpdateUserEmail(string alias, string email, out string error)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "UPDATE [Tbl_User] SET [Us_Email] = @email WHERE [Us_Alias] = @alias";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);


            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;
            dbCommand.Parameters.Add("email", SqlDbType.NVarChar, 30).Value = email;


            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Det uppdaterades inte i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public int DeleteUser(string alias, out string error)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String deleteSQL = "deleteUser @alias";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(deleteSQL, dbConnection);


            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;


            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Det uppdaterades inte i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public List<UserDetail> SelectUserList(out string errormsg, string alias)
        {   // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT * FROM myPage2 WHERE Us_Alias = @alias;";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;

            SqlDataReader reader = null;

            List<UserDetail> Userlist = new List<UserDetail>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    UserDetail UserDetail = new UserDetail();
                    UserDetail.Us_Fname = reader["Us_Fname"].ToString();
                    UserDetail.Us_Lname = reader["Us_Lname"].ToString();
                    UserDetail.Us_Alias = reader["Us_Alias"].ToString();
                    UserDetail.Us_Age = Convert.ToInt16(reader["Us_Age"]);
                    UserDetail.Us_Email = reader["Us_Email"].ToString();
                    UserDetail.Us_Description = reader["Us_Description"].ToString();

                    Userlist.Add(UserDetail);
                }
                reader.Close();
                return Userlist;
            }
            catch (Exception e)
            {   errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public int LogInUser(string alias, string email, out string error)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();


            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String selectSQL = "SELECT COUNT(*) FROM [Tbl_User] WHERE [Us_Alias] = @alias AND [Us_Email] = @email";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);


            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;
            dbCommand.Parameters.Add("email", SqlDbType.NVarChar, 30).Value = email;


            try
            {
                dbConnection.Open();
                int userCount = (int)dbCommand.ExecuteScalar();
                if (userCount == 1)
                {
                    error = "";
                }
                else { error = "Ange korrekt alias och email eller skapa ett nytt konto"; }
                return (userCount);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int UpdateUserDescription(string alias, string description, out string error)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HaikusDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String insertSQL = "UPDATE [Tbl_User] SET [Us_Description] = @description WHERE [Us_Alias] = @alias";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);


            dbCommand.Parameters.Add("alias", SqlDbType.NVarChar, 30).Value = alias;
            dbCommand.Parameters.Add("description", SqlDbType.NVarChar, 100).Value = description;


            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Det uppdaterades inte i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
