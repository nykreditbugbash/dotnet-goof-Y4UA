using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Dynamic;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection11
    { 
        public void Run()
        {
            using (var context = new SchoolContext())
            {
                // Secure: Use strongly-typed LINQ expressions to avoid SQL injection
                string name = System.Console.ReadLine();
                var studentList = context.Students.Where(s => s.Name == name);

                // Secure: Use strongly-typed LINQ expressions
                var studentList2 = context.Students.Where(s => s.Name == name);

                // Secure: Use strongly-typed LINQ expressions for selection
                var studentList3 = context.Students.Select(s => s.Name);

                // Secure: Use strongly-typed OrderBy with a switch or if-else
                bool ascending = true; // or determine based on user input safely
                var studentList4 = ascending
                    ? context.Students.OrderBy(s => s.Name)
                    : context.Students.OrderByDescending(s => s.Name);
            }
        }
    }


}
