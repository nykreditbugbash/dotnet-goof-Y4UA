using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection8
    {
        public void Run()
        {
            var cn = new SqliteConnection("");

            try
            {
                cn.Open();
                // Fixed: Use parameterized query to prevent SQL injection
                var cmd = new SqliteCommand("select count(*) from Users where ID = @id", cn);
                cmd.Parameters.AddWithValue("@id", System.Console.ReadLine());
                string result = cmd.ExecuteScalar().ToString();
            }
            finally
            {
                if (cn.State != System.Data.ConnectionState.Closed) 
                { 
                    cn.Close(); 
                }
            }
        }
    }
}
