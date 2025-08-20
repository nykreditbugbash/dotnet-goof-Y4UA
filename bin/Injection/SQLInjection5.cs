using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Text;


namespace NETStandaloneBlot.Injection
{
    class SQLInjection5
    {
        public void Run()
        {
            using (var context = new SchoolContext())
            {
                // FIXED: Use parameterized query to prevent SQL Injection
                string name = System.Console.ReadLine();
                var studentList = context.Students
                                    .SqlQuery("SELECT * FROM users WHERE ( name = @p0)", name)
                                    .ToListAsync();
            }


            using (ObjectContext studentContext = new ObjectContext("name=StudentEntities"))
            {

                string name = System.Console.ReadLine();

                // FIXED: Use parameterized query instead of passing user input directly
                string studentName = System.Console.ReadLine();
                ObjectQuery<Student> studentQuery = studentContext.CreateQuery<Student>(
                    "SELECT VALUE s FROM students AS s WHERE s.Name = @name", 
                    new ObjectParameter("name", studentName)
                );

                ObjectQuery<Student> studentQuery2 = new ObjectQuery<Student>("students", studentContext, MergeOption.NoTracking);

                // FIXED: Validate or whitelist the path before using it in Include
                string path = System.Console.ReadLine();
                // Example whitelist check (adjust as needed)
                var allowedPaths = new List<string> { "Courses", "Grades" };
                IEnumerable<Student> students = allowedPaths.Contains(path)
                    ? studentQuery2.Include<Student>(path)
                    : studentQuery2;

                // FIXED: Use parameterized query for where clause
                string whereValue = System.Console.ReadLine();
                ObjectQuery<Student> studentQuery4 = studentQuery2.Where("it.Name = @name", new ObjectParameter("name", whereValue));

                // FIXED: Avoid using raw user input as SQL
                string sql = System.Console.ReadLine();
                // Instead, use parameterized query or validate/whitelist the input
                if (sql == "SELECT VALUE s FROM students AS s")
                {
                    studentContext.CreateQuery<Student>(sql);
                    studentContext.CreateQuery<Student>(sql, null);
                }
            }

            string db = System.Console.ReadLine();
            
            // CTSECISSUE: SQLInjection
            using (ObjectContext studentContext = new ObjectContext("name=" + db))
            {
                // FIXED: Use parameterized queries to prevent SQL Injection
                string name = System.Console.ReadLine();
                studentContext.ExecuteStoreCommand("SELECT * FROM users WHERE ( name = @p0)", name);
                
                studentContext.ExecuteStoreCommand(TransactionalBehavior.EnsureTransaction, "SELECT * FROM users WHERE ( name = @p0)", name);

                studentContext.ExecuteStoreCommand(TransactionalBehavior.EnsureTransaction, "SELECT * FROM users WHERE ( name = @p0)", name);

                studentContext.ExecuteStoreQuery<Student>("SELECT * FROM users WHERE ( name = @p0)", name);

                studentContext.ExecuteStoreQuery<Student>("SELECT * FROM users WHERE ( name = @p0)", MergeOption.AppendOnly, name);

                studentContext.ExecuteStoreQuery<Student>("SELECT * FROM users WHERE ( name = @p0)", new ExecutionOptions(MergeOption.AppendOnly, false), name);
            }
        }
    }
}
