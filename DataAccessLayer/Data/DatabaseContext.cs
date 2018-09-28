namespace DataAccessLayer
{
    using DataAccessLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseContext : DbContext
    {
        // Your context has been configured to use a 'DatabaseContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccessLayer.DatabaseContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseContext' 
        // connection string in the application configuration file.
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Answer> answers { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<GivenAnswer> givenAnswers { get; set; }
        public virtual DbSet<Question> questions { get; set; }
        public virtual DbSet<QuestionType> questionTypes { get; set; }
        public virtual DbSet<Test> tests { get; set; }
        public virtual DbSet<TestTemplate> testTemplates { get; set; }
        public virtual DbSet<TestTemplates_Questions> testTemplates_Questions { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}