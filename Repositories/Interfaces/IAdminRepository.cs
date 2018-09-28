using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace Repositories
{
    public interface IAdminRepository
    {
        void AddTest(Test entity);


        void DeleteTest(int? id);


        void AddTestTemplate(TestTemplate entity);

        void AddAnswer(Answer entity);

        Test GetTest(int? id);

        void EditQuestion(Question editedQuestion);

        void AddQuestion(Question entity);


        void RemoveQuestion(int? id);

        GivenAnswer GetGivenAnswer(int? testID, int questionID);

        Question GetQuestion(int? id);

        IEnumerable<Question> GetQuestionsByTestId(int? id);

        IEnumerable<Answer> GetAnswersByQuestionId(int? id);
        IEnumerable<GivenAnswer> GetGivenAnswers(int? testID, int questionID);
        

        void RemoveAnswer(int? id);

        Answer EditAnswer(Answer editedAnswer);

        Answer GetAnswer(int? id);

        void AddQuestionsToTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate);

        void RemoveTestTemplate(int? id);

        TestTemplate EditTestTemplate(IEnumerable<Question> _questions, TestTemplate _testTemplate);

       
        Question EditQuestion(Question editedQuestion, IEnumerable<Answer> answers);
        Question AddAnswersToQuestion(IEnumerable<Answer> answers, Question question);
        Test CreateTest(int testTemplateID, string username, string testKey,
            int timeLimit, DateTime expirationDate);
        IEnumerable<Question> GetAllQuestions();
        TestTemplate GetTestTemplate(int? id);

        
      
        Category GetCategoryName(int categoryID);
        QuestionType GetQuestionType(int questionTypeID);
        
        IEnumerable<Category> GetAllCategories();
        IEnumerable<QuestionType> GetAllQuestionTypes();

        IEnumerable<GivenAnswer> GetGivenAnswers(int testID, int questionID);
        IEnumerable<GivenAnswer> GetAllGivenAnswersByTestID(int? testID);

        void setOpenAnswerScore(int givenAnswerID, int points);
        void SetAnswerCorrectness(int givenAnswerID, bool isCorrect);
        void UpdateTestScore(int testID, int percentageScore);
        ICollection<Question> GetQuestionsFromTemplate(int testTemplateID);

        IEnumerable<TestTemplate> GetAllTestTemplates();

        IEnumerable<Test> GetAllTests();
        IEnumerable<Test> GetAllFinishedTests();
        GivenAnswer GetGivenAnswer(int givenAnswerID);
    }
}
