﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace HaikuLab3.Models
{
    public class UserMethods
    {
        public UserMethods () {}

        
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


    }
}
