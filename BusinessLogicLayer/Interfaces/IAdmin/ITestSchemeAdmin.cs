
using DataAccessLayer.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.IAdmin
{
    public interface ITestSchemeAdmin
    {
        IEnumerable<Question> GetAllQuestions();

        TestTemplate CreateTestScheme(string templateName, string templateDescription, List<int> questionIDList);


        TestTemplates_Questions CreateTestTemplateQuestionsConnection(int testTemplateID, int questionID);


        void AddQuestionsToTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate);

        TestTemplate GetTestTemplate(int? id);

        void AddTestScheme(TestTemplate testTemplate);

        void DeleteTestScheme(int? id);
        string GetQuestionTypeName(int questionID);



        TestTemplate EditTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate);

        IEnumerable<TestTemplate> GetTestTemplates();

        void AddTemplateQuestions(List<Question> qList, TestTemplate testTemplate);

        List<Question> GetQuestionListFromID(List<int> questionIDList);
    }
}
