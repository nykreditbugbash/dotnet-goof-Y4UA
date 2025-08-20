using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Search.Query;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;

namespace NETMVCBlot.Controllers
{
    public class SQLInjController : Controller
    {
        public ActionResult Index(string input)
        {
            using (ObjectContext studentContext = new ObjectContext("name=StudentEntities"))
            {
                // FIXED: Use parameterized query to prevent SQL Injection
                var query = studentContext.CreateQuery<Student>(
                    "select * from students where Name = @name",
                    new ObjectParameter("name", input));

                // FIXED: Use parameterized command
                studentContext.ExecuteStoreCommand(
                    "select * from students where Name = @name",
                    new System.Data.SqlClient.SqlParameter("@name", input));

                // FIXED: Use parameterized query
                studentContext.ExecuteStoreQuery<Student>(
                    "select * from students where Name = @name",
                    new System.Data.SqlClient.SqlParameter("@name", input));

                // FIXED: Use parameterized query with MergeOption
                studentContext.ExecuteStoreQuery<Student>(
                    "select * from students where Name = @name",
                    new System.Data.SqlClient.SqlParameter("@name", input),
                    MergeOption.AppendOnly);
            }

            // For FullTextSqlQuery, validate or sanitize input before use
            string safeInput = input.Replace("'", "''"); // Basic escaping, consider stricter validation
            FullTextSqlQuery myQuery = new FullTextSqlQuery(SPContext.Current.Site)
            {
                // FIXED: Use sanitized input
                QueryText = "SELECT Path FROM SCOPE() WHERE  \"SCOPE\" = '" + safeInput + "'",
                ResultTypes = ResultType.RelevantResults

            };

            return View();
        }
    }

    class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }

    class Student
    {
    }
}