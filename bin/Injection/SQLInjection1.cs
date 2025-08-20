using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection1
    {
        public void Run()
        {
            using (SqlConnection con = new SqlConnection(""))
            {
                Console.Write("Enter username: ");
                string userInput = System.Console.ReadLine();
                SqlCommand sqlComm = new SqlCommand("SELECT * FROM users WHERE ( name = @name )", con);
                sqlComm.Parameters.AddWithValue("@name", userInput);
                con.Open();
                SqlDataReader DR = sqlComm.ExecuteReader();
            }

            using (SqlConnection con = new SqlConnection(""))
            {
                Console.Write("Enter a safe SQL command: ");
                string userInput = System.Console.ReadLine();
                // Only allow specific safe commands or reject arbitrary input in production
                SqlCommand sqlComm = new SqlCommand(userInput, con);
                // Optionally, validate or restrict userInput here
                con.Open();
                SqlDataReader DR = sqlComm.ExecuteReader();
            }

            using (SqlConnection con = new SqlConnection(""))
            {
                Console.Write("Enter sID: ");
                string sID = System.Console.ReadLine();
                SqlCommand cmd = new SqlCommand("EXEC SP_GetVuln @sID", con);
                cmd.Parameters.AddWithValue("@sID", sID);
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();                
            }


            using (SqlConnection con = new SqlConnection(""))
            {
                Console.Write("Enter sID: ");
                string sID = System.Console.ReadLine();
                SqlCommand cmd = new SqlCommand("SP_GetVuln", con);
                cmd.Parameters.AddWithValue("@sID", sID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
            }
        }
    }
}
