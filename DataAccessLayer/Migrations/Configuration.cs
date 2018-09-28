namespace DataAccessLayer.Migrations
{
    using DataAccessLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Category cat1 = new Category { categoryID = 1, categoryName = "C#" };
            Category cat2 = new Category { categoryID = 2, categoryName = "C++" };
            Category cat3 = new Category { categoryID = 3, categoryName = "Java" };
            Category cat4 = new Category { categoryID = 4, categoryName = "JavaScript" };
            Category cat5 = new Category { categoryID = 5, categoryName = "Python" };

            QuestionType qt1 = new QuestionType { questionTypeID = 1, typeName = "Otwarte" };
            QuestionType qt2 = new QuestionType { questionTypeID = 2, typeName = "Jednokrotnego wyboru" };
            QuestionType qt3 = new QuestionType { questionTypeID = 3, typeName = "Wielokrotnego wyboru" };

            Question qu1 = new Question { questionID = 1, value = "Pytanie 1", categoryID = cat1.categoryID, points = 1, questionTypeID = qt1.questionTypeID };
            Question qu2 = new Question { questionID = 2, value = "Pytanie 2", categoryID = cat2.categoryID, points = 2, questionTypeID = qt2.questionTypeID };
            Question qu3 = new Question { questionID = 3, value = "Pytanie 3", categoryID = cat3.categoryID, points = 3, questionTypeID = qt3.questionTypeID };

            Answer a1 = new Answer { answerID = 1, value = "odpowiedz 1", isCorrect = true, questionID = 2 };
            Answer a2 = new Answer { answerID = 2, value = "odpowiedz 2", isCorrect = false, questionID = 2 };

            TestTemplate tt1 = new TestTemplate { testTemplateID = 1, templateName = "Szablon 1", description = "Opis1"  };
            

            TestTemplates_Questions ttq1 = new TestTemplates_Questions {testTemplates_QuestionsID=1, testTemplateID = tt1.testTemplateID, questionID = qu1.questionID };
            Test t1 = new Test
            {
                testID = 1,
                isCompleted = false,
                testKey = "testkey123",
                completionDate = null,
                expirationDate = new DateTime(2020, 1, 1),
                testTemplateID = 1,
                completionTime = null,
                username = "Kamil Rutkowski",
                timeLimit = 1,
                percentageScore = null,
                isReviewed = false
            };

            context.categories.AddOrUpdate(cat1);
            context.categories.AddOrUpdate(cat2);
            context.categories.AddOrUpdate(cat3);
            context.categories.AddOrUpdate(cat4);
            context.categories.AddOrUpdate(cat5);

            context.questionTypes.AddOrUpdate(qt1);
            context.questionTypes.AddOrUpdate(qt2);
            context.questionTypes.AddOrUpdate(qt3);

            context.questions.AddOrUpdate(qu1);
            context.questions.AddOrUpdate(qu2);
            context.questions.AddOrUpdate(qu3);

            context.answers.AddOrUpdate(a1);
            context.answers.AddOrUpdate(a2);

            context.testTemplates.AddOrUpdate(tt1);

            context.testTemplates_Questions.AddOrUpdate(ttq1);

            context.tests.AddOrUpdate(t1);
            context.SaveChanges();
        }
    }
}
