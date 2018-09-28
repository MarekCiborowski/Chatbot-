using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces.IAdmin;
using DataAccessLayer.Models;
using Repositories;

namespace BusinessLogicLayer.Services.AdminServices
{
    public class TestSchemeAdmin : ITestSchemeAdmin
    {
        private IAdminRepository _adminRepository;

        public TestSchemeAdmin(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _adminRepository.GetAllQuestions();
        }

        public void AddTestScheme(TestTemplate testTemplate)
        {
            _adminRepository.AddTestTemplate(testTemplate);
        }
        public TestTemplate CreateTestScheme(string templateName, string templateDescription, List<int> questionIDList)
        {
            TestTemplate testTemplate = new TestTemplate();
            testTemplate.templateName = templateName;
            testTemplate.description = templateDescription;

            List<Question> qList = GetQuestionListFromID(questionIDList);

            return testTemplate;
        }

        public TestTemplates_Questions CreateTestTemplateQuestionsConnection(int testTemplateID, int questionID)
        {
            return new TestTemplates_Questions()
            {
                testTemplateID = testTemplateID,
                questionID = questionID,
            };
        }

        public void AddQuestionsToTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate)
        {
            _adminRepository.AddQuestionsToTestScheme(questions, testTemplate);
               
        }

        public void DeleteTestScheme(int? id)
        {
            _adminRepository.RemoveTestTemplate(id);
        }

        public TestTemplate EditTestScheme(IEnumerable<Question> questions, TestTemplate testTemplate)
        {
            return _adminRepository.EditTestTemplate(questions, testTemplate);
        }

        public TestTemplate GetTestTemplate(int? id)
        {
            return _adminRepository.GetTestTemplate(id);
        }

        public string GetQuestionTypeName(int questionID)
        {
            return _adminRepository.GetQuestionType(questionID).typeName;
        }

        public IEnumerable<TestTemplate> GetTestTemplates()
        {
            return _adminRepository.GetAllTestTemplates();
        }

        public void AddTemplateQuestions(List<Question> qList, TestTemplate testTemplate)
        {
            _adminRepository.AddQuestionsToTestScheme(qList.AsEnumerable(), testTemplate);
        }

        public List<Question> GetQuestionListFromID(List<int> questionIDList)
        {
            List<Question> qList = new List<Question>();
            foreach (int id in questionIDList)
            {
                qList.Add(_adminRepository.GetQuestion(id));
            }

            return qList;
        }

    }
}
