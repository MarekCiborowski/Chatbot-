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

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext db;

        public UserRepository(DatabaseContext _db)
        {
            db = _db;
        }

        public int? GetTestByKey(string testKey)
        {
            if (!db.tests.Any(o => o.testKey == testKey))
                return -1;
            //throw new Exception("Invalid test key");
            if (DateTime.Compare( db.tests.FirstOrDefault(t => t.testKey == testKey).expirationDate, DateTime.Now) < 0)
                return -2;
            //throw new Exception("Sorry, but test expired");
            if (db.tests.FirstOrDefault(o => o.testKey == testKey).isCompleted)
                return -3;
            //throw new Exception("Test was already solved");
            return db.tests.First(t => t.testKey == testKey).testID;
        }

        public IEnumerable<Question> GetQuestionsByTestId(int id)
        {
            int templateId = db.tests.First(t => t.testID == id).testTemplateID;

            IEnumerable<TestTemplates_Questions> questionsTemplates = db.testTemplates_Questions.Where(t => t.testTemplate.testTemplateID == templateId);

            List<Question> questions = new List<Question>();
            foreach (TestTemplates_Questions qt in questionsTemplates)
            {
                questions.Add(db.questions.First(q=>q.questionID==qt.questionID));
            }
            return questions;
        }


        public GivenAnswer CreateGivenAnswer(int testID, int questionID, string answer)
        {
            return new GivenAnswer()
            {
                testID = testID,
                questionID = questionID,
                answer = answer
            };
        }

        public void AddGivenAnswer(GivenAnswer givenAnswer)
        {
            db.givenAnswers.Add(givenAnswer);
            SaveChanges();
        }

        public GivenAnswer EditGivenAnswer(GivenAnswer givenAnswer, string newAnswer)
        {
            givenAnswer.answer = newAnswer;
            SaveChanges();
            return givenAnswer;
        }

        private void SaveChanges()
        {
            db.SaveChanges();
        }
        public void FinalizingTest(int testID)//, TimeSpan completionTime)
        {
            Test test = db.tests.FirstOrDefault(t => t.testID == testID);
            //test.completionTime = completionTime;
          //  test.isCompleted = true;
            test.completionDate = DateTime.Now;
            CheckAllAnswersInTest(testID);
            SaveChanges();
        }

        public IEnumerable<Answer> GetAnswersForQuestion(int questionID)
        {
            return db.answers.Where(q => q.questionID == questionID);
        }




        private bool CheckAnswer(string answer, int questionID)
        {
            //foreach (Answer _answer in db.questions.Find(questionID).answer)
            foreach(Answer _answer in db.answers.Where(q => q.questionID == questionID))
            {
                if (_answer.value.ToLower() == answer.ToLower())
                    return _answer.isCorrect;
            }
            return false;
        }

        private void CheckAllAnswersInTest(int testID)
        {
            IEnumerable<GivenAnswer> givenAnswers = db.givenAnswers.Where(t => t.testID == testID);
            foreach (GivenAnswer givenAnswer in givenAnswers)
            {
                if (GetQuestionTypeID(givenAnswer.questionID) != 1)
                    givenAnswer.isCorrect = CheckAnswer(givenAnswer.answer, givenAnswer.questionID);
            }
            
        }
        public QuestionType GetQuestionType(int questionTypeID)
        {
            QuestionType q = db.questionTypes.FirstOrDefault(s=>s.questionTypeID == questionTypeID);
            return q;
        }
        public int GetQuestionTypeID(int questionID)
        {
           
            return db.questions.FirstOrDefault(q=>q.questionID==questionID).questionTypeID;
        }



    }
}
