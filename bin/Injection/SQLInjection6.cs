using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Text;



namespace NETStandaloneBlot.Injection
{
    class SQLInjection6
    {
        public void Run()
        {
            using (var context = new StudentContext())
            {
                // FIXED: Use parameterized query to prevent SQL Injection
                var name = System.Console.ReadLine();
                var gradeList = context.Database.ExecuteSqlCommand(
                    "SELECT * FROM users WHERE ( name = @p0 )", name);
            }

            using (var context = new StudentContext())
            {
                // FIXED: Use parameterized query to prevent SQL Injection
                var name = System.Console.ReadLine();
                var gradeList = context.Database.ExecuteSqlCommandAsync(
                    "SELECT * FROM users WHERE ( name = @p0 )", name).Result;
            }
        }
    }

    class StudentContext : DbContext
    {
        //public StudentContext() : base("StudentDB")
        //{
        //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentContext, NETStandaloneBlot.Migrations.Configuration>());
        //}

        public DbSet<Grade> Grades { get; set; }
    }

    class Grade
    {
    }
}
