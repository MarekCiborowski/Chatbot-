namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        answerID = c.Int(nullable: false, identity: true),
                        value = c.String(),
                        isCorrect = c.Boolean(nullable: false),
                        questionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.answerID)
                .ForeignKey("dbo.Question", t => t.questionID, cascadeDelete: true)
                .Index(t => t.questionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        questionID = c.Int(nullable: false, identity: true),
                        value = c.String(),
                        points = c.Int(nullable: false),
                        categoryID = c.Int(nullable: false),
                        questionTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.questionID)
                .ForeignKey("dbo.Category", t => t.categoryID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionType", t => t.questionTypeID, cascadeDelete: true)
                .Index(t => t.categoryID)
                .Index(t => t.questionTypeID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        categoryName = c.String(),
                    })
                .PrimaryKey(t => t.categoryID);
            
            CreateTable(
                "dbo.GivenAnswer",
                c => new
                    {
                        givenAnswerID = c.Int(nullable: false, identity: true),
                        answer = c.String(),
                        isCorrect = c.Boolean(nullable: false),
                        testID = c.Int(nullable: false),
                        questionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.givenAnswerID)
                .ForeignKey("dbo.Question", t => t.questionID, cascadeDelete: true)
                .ForeignKey("dbo.Test", t => t.testID, cascadeDelete: true)
                .Index(t => t.testID)
                .Index(t => t.questionID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        testID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        testKey = c.String(),
                        percentageScore = c.Int(nullable: false),
                        timeLimit = c.Int(nullable: false),
                        isCompleted = c.Boolean(nullable: false),
                        completionDate = c.DateTime(nullable: false),
                        completionTime = c.Time(nullable: false, precision: 7),
                        expirationDate = c.DateTime(nullable: false),
                        testTemplateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.testID)
                .ForeignKey("dbo.TestTemplate", t => t.testTemplateID, cascadeDelete: true)
                .Index(t => t.testTemplateID);
            
            CreateTable(
                "dbo.TestTemplate",
                c => new
                    {
                        testTemplateID = c.Int(nullable: false, identity: true),
                        templateName = c.String(),
                    })
                .PrimaryKey(t => t.testTemplateID);
            
            CreateTable(
                "dbo.TestTemplates_Questions",
                c => new
                    {
                        testTemplates_QuestionsID = c.Int(nullable: false, identity: true),
                        testTemplateID = c.Int(nullable: false),
                        questionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.testTemplates_QuestionsID)
                .ForeignKey("dbo.Question", t => t.questionID, cascadeDelete: true)
                .ForeignKey("dbo.TestTemplate", t => t.testTemplateID, cascadeDelete: true)
                .Index(t => t.testTemplateID)
                .Index(t => t.questionID);
            
            CreateTable(
                "dbo.QuestionType",
                c => new
                    {
                        questionTypeID = c.Int(nullable: false, identity: true),
                        typeName = c.String(),
                    })
                .PrimaryKey(t => t.questionTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answer", "questionID", "dbo.Question");
            DropForeignKey("dbo.Question", "questionTypeID", "dbo.QuestionType");
            DropForeignKey("dbo.GivenAnswer", "testID", "dbo.Test");
            DropForeignKey("dbo.Test", "testTemplateID", "dbo.TestTemplate");
            DropForeignKey("dbo.TestTemplates_Questions", "testTemplateID", "dbo.TestTemplate");
            DropForeignKey("dbo.TestTemplates_Questions", "questionID", "dbo.Question");
            DropForeignKey("dbo.GivenAnswer", "questionID", "dbo.Question");
            DropForeignKey("dbo.Question", "categoryID", "dbo.Category");
            DropIndex("dbo.TestTemplates_Questions", new[] { "questionID" });
            DropIndex("dbo.TestTemplates_Questions", new[] { "testTemplateID" });
            DropIndex("dbo.Test", new[] { "testTemplateID" });
            DropIndex("dbo.GivenAnswer", new[] { "questionID" });
            DropIndex("dbo.GivenAnswer", new[] { "testID" });
            DropIndex("dbo.Question", new[] { "questionTypeID" });
            DropIndex("dbo.Question", new[] { "categoryID" });
            DropIndex("dbo.Answer", new[] { "questionID" });
            DropTable("dbo.QuestionType");
            DropTable("dbo.TestTemplates_Questions");
            DropTable("dbo.TestTemplate");
            DropTable("dbo.Test");
            DropTable("dbo.GivenAnswer");
            DropTable("dbo.Category");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
        }
    }
}
