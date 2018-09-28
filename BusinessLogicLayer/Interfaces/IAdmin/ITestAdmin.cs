using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.IAdmin
{
    public interface ITestAdmin
    {
        void AddTest(Test entity);

        void DeleteTest(int? id);
        GivenAnswerVM GetGivenAnswer(int? testID, int questionID);

        Test CreateTest(int testTemplateID, string username, string testKey,
            int timeLimit, DateTime expirationDate);

        TestVM GetTestVM(int? id);
        Test GetTest(int? id);
        IEnumerable<TestVM> GetAllTestsVM();

        IEnumerable<GivenAnswerVM> GetGivenAnswers(int? testID, int questionID);
        IEnumerable<TestTemplate> GetTestTemplates();
        IEnumerable<GivenAnswerVM> GetAllGivenAnswersByTestID(int? testID);
        IEnumerable<QuestionVM> GetQuestionsByTestId(int? id);
        string RandomString();

        List<Test> GetAllTests();
        IEnumerable<TestVM> GetAllFinishedTests();

        TestTemplate GetTestTemplate(int id);
        GivenAnswerVM GetGivenAnswer(int givenAnswerID);
        void setOpenAnswerScore(int givenAnswerID, int points);
    }
}
