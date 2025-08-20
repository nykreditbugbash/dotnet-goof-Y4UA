using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection3
    {
        public void Run()
        {
            using (SqlConnection con = new SqlConnection(""))
            {
                string userInput = System.Console.ReadLine();
                string commandText = "SELECT * FROM users WHERE ( name = @name )";
                SqlCommand sqlComm = new SqlCommand(commandText, con);
                sqlComm.Parameters.AddWithValue("@name", userInput);
                con.Open();
                SqlDataReader DR = sqlComm.ExecuteReader();
            }
        }
    }
}
