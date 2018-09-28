using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Entity;
using DataAccessLayer;
using Autofac;
using System.Reflection;
using DataAccessLayer.Models;
using System.Collections.ObjectModel;

namespace Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private DatabaseContext db;

        public AdminRepository(DatabaseContext _db)
        {
            db = _db;
        }



        public void AddTestTemplate(TestTemplate entity)
        {
            db.testTemplates.Add(entity);
            SaveChanges();
        }


        public void AddQuestion(Question entity)
        {
            db.questions.Add(entity);
            SaveChanges();
        }

        // usuwanie elementow w tabeli laczacej (czy leci kaskadowo?)
        public void RemoveQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");

            Question question = db.questions.Find(id);


            foreach (TestTemplates_Questions testTemplateQuestion in question.testTemplates_Questions)
            {
                RemoveTestTemplateQuestion(testTemplateQuestion.testTemplates_QuestionsID);

            }


            foreach (GivenAnswer givenAnswer in db.givenAnswers)
            {
                if (givenAnswer.questionID == id)
                    RemoveGivenAnswer(id);
            }

            db.questions.Remove(question);
            SaveChanges();
        }

        public Question GetQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.questions.FirstOrDefault(q => q.questionID == id);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return db.questions;
        }

        public IEnumerable<TestTemplate> GetAllTestTemplates()
        {
            return db.testTemplates;
        }

        public IEnumerable<Test> GetAllTests()
        {
            return db.tests;
        }

        public IEnumerable<Test> GetAllFinishedTests()
        {
            return db.tests.Where(t=>t.isCompleted == true);
        }

        public Question AddAnswersToQuestion(IEnumerable<Answer> answers, Question question)
        {
            foreach (Answer answer in answers)
            {
                question.answer.Add(answer);
            }
            SaveChanges();
            return question;
        }

        public IEnumerable<Answer> GetAnswersByQuestionId(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.answers.Where(q => q.question.questionID == id);
        }






        private void SaveChanges()
        {
            db.SaveChanges();
        }

        public void RemoveAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Answer answer = db.answers.Find(id);
            db.answers.Remove(answer);
            SaveChanges();
        }

        public Answer EditAnswer(Answer editedAnswer)
        {
            Answer answer = db.answers.Find(editedAnswer.answerID);
            answer.isCorrect = editedAnswer.isCorrect;
            answer.value = editedAnswer.value;
            SaveChanges();
            return answer;


        }

        public Answer GetAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.answers.Find(id);
        }


        public Question EditQuestion(Question editedQuestion, IEnumerable<Answer> answers)
        {
            Question question = db.questions.Find(editedQuestion.questionID);
            question.points = editedQuestion.points;
            question.questionTypeID = editedQuestion.questionTypeID;
            question.categoryID = editedQuestion.categoryID;
            question.value = editedQuestion.value;
            foreach (Answer answer in question.answer)
            {
                RemoveAnswer(answer.answerID);
            }
            AddAnswersToQuestion(answers, question);
            SaveChanges();
            return question;
        }

        public void EditQuestion(Question editedQuestion)
        {
            db.Entry(editedQuestion).State = EntityState.Modified;
            db.SaveChanges();
        }


        public void AddQuestionsToTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate)
        {
            foreach (Question _question in questions)
            {
                TestTemplates_Questions testTemplates_Questions = new TestTemplates_Questions
                {
                    questionID = _question.questionID,
                    testTemplateID = testTemplate.testTemplateID,
                };
                db.testTemplates_Questions.Add(testTemplates_Questions);
            }
            SaveChanges();
        }

        // spytać Mariusza czy SaveChanges() ma być w RemoveTestTemplateQuestion
        // spytać Mariusza czy SaveChanges() ma być w RemoveTestTemplateQuestion
        // spytać Mariusza czy SaveChanges() ma być w RemoveTestTemplateQuestion
        public void RemoveTestTemplate(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            TestTemplate testTemplate = db.testTemplates.Find(id);
            foreach (TestTemplates_Questions testTemplateQuestion in testTemplate.testTemplates_Questions)
            {
                RemoveTestTemplateQuestion(testTemplateQuestion.testTemplates_QuestionsID);
            }
            db.testTemplates.Remove(testTemplate);
            SaveChanges();
        }

        private void RemoveTestTemplateQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            TestTemplates_Questions testTemplates_Questions = db.testTemplates_Questions.Find(id);
            db.testTemplates_Questions.Remove(testTemplates_Questions);

        }

        public TestTemplate EditTestTemplate(IEnumerable<Question> _questions, TestTemplate _testTemplate)
        {
            TestTemplate testTemplate = db.testTemplates.Find(_testTemplate.testTemplateID);
            testTemplate.templateName = _testTemplate.templateName;
            foreach (TestTemplates_Questions testTemplateQuestion in testTemplate.testTemplates_Questions)
            {
                RemoveTestTemplateQuestion(testTemplateQuestion.testTemplates_QuestionsID);
            }
            AddQuestionsToTestScheme(_questions, testTemplate);

            SaveChanges();

            return testTemplate;
        }


        public void AddTest(Test entity)
        {
            db.tests.Add(entity);
            SaveChanges();
        }
        public void DeleteTest(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            foreach (GivenAnswer givenAnswer in db.givenAnswers.ToList())
            {
                if (givenAnswer.questionID == id)
                    RemoveGivenAnswer(id);
            }
            db.tests.Remove(db.tests.Find(id));
            SaveChanges();
        }

        public Test CreateTest(int testTemplateID, string username, string testKey,
            int timeLimit, DateTime expirationDate)
        {
            return new Test()
            {
                testTemplateID = testTemplateID,
                username = username,
                testKey = testKey,
                timeLimit = timeLimit,
                expirationDate = expirationDate,
            };

        }

        public Test GetTest(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.tests.Find(id);
        }

        public TestTemplate GetTestTemplate(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.testTemplates.Find(id);
        }


        


        private void RemoveGivenAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            db.givenAnswers.Remove(db.givenAnswers.Find(id));
        }

        public Category GetCategoryName(int categoryID)
        {
            return db.categories.FirstOrDefault(c=>c.categoryID == categoryID);
        }

        public QuestionType GetQuestionType(int questionTypeID)
        {
            return db.questionTypes.FirstOrDefault(q=>q.questionTypeID == questionTypeID);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.categories;
        }

        public IEnumerable<QuestionType> GetAllQuestionTypes()
        {
            return db.questionTypes;
        }

        public IEnumerable<GivenAnswer> GetGivenAnswers(int testID, int questionID)
        {
            return db.givenAnswers.Where(t => t.testID == testID && t.questionID == questionID);
        }

        public IEnumerable<GivenAnswer> GetAllGivenAnswersByTestID(int? testID)
        {
            if (testID == null)
                throw new ArgumentNullException("Null argument");
            return db.givenAnswers.Where(t => t.testID == testID);
        }
              

        public void setOpenAnswerScore(int givenAnswerID, int points)
        {
            GivenAnswer givenAnswer = db.givenAnswers.FirstOrDefault(t => t.givenAnswerID == givenAnswerID);
            givenAnswer.points = points;
            SaveChanges();
        }

        public void SetAnswerCorrectness(int givenAnswerID, bool isCorrect)
        {
            GivenAnswer givenAnswer = db.givenAnswers.FirstOrDefault(g => g.givenAnswerID == givenAnswerID);
            givenAnswer.isCorrect = isCorrect;
            SaveChanges();
        }

        public void UpdateTestScore(int testID, int percentageScore)
        {
            Test test = db.tests.FirstOrDefault(t => t.testID == testID);
            test.percentageScore = percentageScore;
            SaveChanges();
        }

        public ICollection<Question> GetQuestionsFromTemplate(int testTemplateID)
        {
            IEnumerable<TestTemplates_Questions> testTemplates_Questions = db.testTemplates_Questions.Where(t => t.testTemplateID == testTemplateID);
            ICollection<Question> questions = new Collection<Question>();
            foreach (TestTemplates_Questions testTemplateQuestion in testTemplates_Questions)
            {
                questions.Add(db.questions.Find(testTemplateQuestion.questionID));
            }
            return questions;
        }

        public void AddAnswer(Answer entity)
        {
            db.answers.Add(entity);
            SaveChanges();
        }

        public IEnumerable<Question> GetQuestionsByTestId(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");

            //int templateId = db.tests.First(t => t.testID == id).testID;
            int templateId = db.tests.First(t => t.testID == id).testTemplateID;

            IEnumerable<TestTemplates_Questions> questionsTemplates = db.testTemplates_Questions.Where(t => t.testTemplate.testTemplateID == templateId);

            List<Question> questions = new List<Question>();
            foreach (TestTemplates_Questions qt in questionsTemplates)
            {
                questions.Add(db.questions.FirstOrDefault(q=>q.questionID==qt.questionID));
            }
            return questions;
        }

        public GivenAnswer GetGivenAnswer(int? testID, int questionID)
        {
            if (testID == null)
                throw new ArgumentNullException("Null argument");

            return db.givenAnswers.Where(g => g.testID == testID).Where(g => g.questionID == questionID).FirstOrDefault();
        }

        public IEnumerable<GivenAnswer> GetGivenAnswers(int? testID, int questionID)        
        {
            if (testID == null)
                throw new ArgumentNullException("Null argument");

            return db.givenAnswers.Where(g => g.testID == testID).Where(g => g.questionID == questionID);
        }

        public GivenAnswer GetGivenAnswer(int givenAnswerID)
        {
            return db.givenAnswers.Where(g => g.givenAnswerID == givenAnswerID).FirstOrDefault();
        }

    }
}
