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
                // Use parameterized queries to prevent SQL injection
                var query = "select * from students where Name = @name";
                var parameter = new System.Data.SqlClient.SqlParameter("@name", input ?? string.Empty);
                studentContext.ExecuteStoreQuery<Student>(query, parameter);

                // Example for ExecuteStoreCommand with parameter
                var command = "select * from students where Name = @name";
                studentContext.ExecuteStoreCommand(command, parameter);

                // Example for CreateQuery with Entity SQL parameters
                var entityQuery = "select value s from students as s where s.Name = @name";
                var entityParameter = new System.Data.Objects.ObjectParameter("name", input ?? string.Empty);
                studentContext.CreateQuery<Student>(entityQuery, entityParameter);

                // Example for ExecuteStoreQuery with MergeOption and parameter
                studentContext.ExecuteStoreQuery<Student>(query, parameter, MergeOption.AppendOnly);
            }

            // Sanitize input for FullTextSqlQuery by using a whitelist or escaping
            string safeInput = input?.Replace("'", "''") ?? string.Empty;
            FullTextSqlQuery myQuery = new FullTextSqlQuery(SPContext.Current.Site)
            {
                QueryText = $"SELECT Path FROM SCOPE() WHERE  \"SCOPE\" = '{safeInput}'",
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
