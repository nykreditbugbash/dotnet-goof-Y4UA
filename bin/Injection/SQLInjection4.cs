using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace NETStandaloneBlot.Injection
{
    class SQLInjection4
    {
        public void Run()
        {
            ServerConnection sc = new ServerConnection
            {
                ConnectionString = ""
            };
            Server server = new Server(sc);
            Console.Write("Enter user name: ");
            string userInput = System.Console.ReadLine();
            string commandText = "SELECT * FROM users WHERE ( name = @name )";
            var cmd = server.ConnectionContext.SqlConnectionObject.CreateCommand();
            cmd.CommandText = commandText;
            cmd.Parameters.AddWithValue("@name", userInput);
            cmd.ExecuteNonQuery();
            server.ConnectionContext.CommitTransaction();
            server.ConnectionContext.Disconnect();
        }
    }
}
