using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection2
    {
        public void Run()
        {
            string userInput = System.Console.ReadLine();
            string commandText = "SELECT * FROM users WHERE ( name = @name)";

            using var connection = new SqlConnection("");
            using var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@name", userInput);

            using var adapter = new SqlDataAdapter(command);
            // CTSECISSUE: SQLInjection fixed by using parameterized query
        }
    }
}
