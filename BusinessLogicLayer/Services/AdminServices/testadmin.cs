using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces.IAdmin;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Repositories;
using BusinessLogicLayer.Services.AdminServices;

namespace BusinessLogicLayer.Services.AdminServices
{
    public class TestAdmin : ITestAdmin
    {
        private IAdminRepository _adminRepository;
        private IMapper _mapper;

        public TestAdmin(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public void AddTest(Test entity)
        {
            _adminRepository.AddTest(entity);
        }
        public void DeleteTest(int? id)
        {
            _adminRepository.DeleteTest(id);
        }
        public Test CreateTest(int testTemplateID, string username, string testKey,
            int timeLimit, DateTime expirationDate)
        {
            return _adminRepository.CreateTest(testTemplateID, username, testKey, timeLimit, expirationDate);
        }
        public Test GetTest(int? id)
        {
            return _adminRepository.GetTest(id);
        }

        public TestVM GetTestVM(int? id)
        {
            Test test = _adminRepository.GetTest(id);
            return (MapTestToViewModel(test));
        }

        
        public void UpdateTest(int testID, int percentageScore)
        {
            _adminRepository.UpdateTestScore(testID, percentageScore);
        }
        public void SetScore(int testID)
        {
            int score = 0;
            int maxScore = 0;
            Test test = GetTest(testID);
            TestTemplate testTemplate = _adminRepository.GetTestTemplate(test.testTemplateID);
            ICollection<Question> questions = _adminRepository.GetQuestionsFromTemplate(testTemplate.testTemplateID);
            foreach (Question question in questions)
            {
                maxScore += question.points;
                if (_adminRepository.GetQuestionType(question.questionTypeID).typeName.ToLower() == "otwarte")
                {
                    foreach (GivenAnswer givenAnswer in _adminRepository.GetGivenAnswers(testID, question.questionID)) //co prawda w otwartym jest jedna odpowiedź, ale tak wyszło xd
                    {
                        score += givenAnswer.points;
                    }
                }
                else //pytanie jest jednokrotnego lub wielokrotnego wyboru
                {
                    bool isCorrect = true;
                    foreach (GivenAnswer givenAnswer in _adminRepository.GetGivenAnswers(testID, question.questionID))
                    {
                        if (!givenAnswer.isCorrect)
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                    if (isCorrect)
                        score += question.points;
                }
            }

            int percentageScore = score * 100 / maxScore;
            UpdateTest(testID, percentageScore);
        }

        public IEnumerable<TestTemplate> GetTestTemplates()
        {
            return _adminRepository.GetAllTestTemplates();
        }

        public string RandomString()
        {
            int length = 14;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public List<Test> GetAllTests()
        {
            return _adminRepository.GetAllTests().ToList();
        }


        // MAPERY

        public Test MapTestToModel(TestVM testVM)
        {
            Test test = _mapper.Map<Test>(testVM);
            return test;
        }

        public TestVM MapTestToViewModel(Test test)
        {
            TestVM testVM = _mapper.Map<TestVM>(test);
            return testVM;
        }

        public GivenAnswer MapGivenAnswerToModel(GivenAnswerVM givenAnswerVM)
        {
            GivenAnswer givenAnswer = _mapper.Map<GivenAnswer>(givenAnswerVM);
            return givenAnswer;
        }

        public GivenAnswerVM MapGivenAnswerToViewModel(GivenAnswer givenAnswer)
        {
            GivenAnswerVM givenAnswerVM = _mapper.Map<GivenAnswerVM>(givenAnswer);
            return givenAnswerVM;
        }

        public Question MapQuestionToModel(QuestionVM questionVM)
        {

            Question question = _mapper.Map<Question>(questionVM);
            return question;
        }

        public QuestionVM MapQuestionToViewModel(Question question)
        {

            QuestionVM questionVM = _mapper.Map<QuestionVM>(question);
            return questionVM;
        }

        //

        public IEnumerable<TestVM> GetAllTestsVM()
        {
            var tests = _adminRepository.GetAllTests();
            List<TestVM> testsVM = new List<TestVM>();

            foreach (var t in tests)
            {
                testsVM.Add(MapTestToViewModel(t));
            }

            return testsVM;
        }

        public IEnumerable<TestVM> GetAllFinishedTests()
        {
            var tests = _adminRepository.GetAllFinishedTests();
            List<TestVM> testsVM = new List<TestVM>();

            foreach(var t in tests)
            {
                testsVM.Add(MapTestToViewModel(t));
            }

            return testsVM;
        }

        public IEnumerable<GivenAnswerVM> GetAllGivenAnswersByTestID(int? testID)
        {            
            var givenAnswers = _adminRepository.GetAllGivenAnswersByTestID(testID);
            List<GivenAnswerVM> givenAnswerVM = new List<GivenAnswerVM>();

            foreach(var g in givenAnswers)
            {
                givenAnswerVM.Add(MapGivenAnswerToViewModel(g));
            }

            return givenAnswerVM;
        }

        public IEnumerable<QuestionVM> GetQuestionsByTestId(int? id)
        {
            var questions = _adminRepository.GetQuestionsByTestId(id);
            List<QuestionVM> questionsVM = new List<QuestionVM>();

            foreach(var q in questions)
            {
                questionsVM.Add(MapQuestionToViewModel(q));
            }

            return questionsVM;
        }

        public GivenAnswerVM GetGivenAnswer(int? testID, int questionID)
        {            
            GivenAnswer givenAnswer = _adminRepository.GetGivenAnswer(testID, questionID);

            return (MapGivenAnswerToViewModel(givenAnswer));
        }

        public IEnumerable<GivenAnswerVM> GetGivenAnswers(int? testID, int questionID)
        {
            var givenAnswers = _adminRepository.GetGivenAnswers(testID, questionID);
            List<GivenAnswerVM> givenAnswersVM = new List<GivenAnswerVM>();

            foreach(var g in givenAnswers)
            {
                givenAnswersVM.Add(MapGivenAnswerToViewModel(g));
            }

            return givenAnswersVM;
        }

        public TestTemplate GetTestTemplate(int id)
        {
            return _adminRepository.GetTestTemplate(id);
        }

        public GivenAnswerVM GetGivenAnswer(int givenAnswerID)
        {
            GivenAnswer g = _adminRepository.GetGivenAnswer(givenAnswerID);

            return (MapGivenAnswerToViewModel(g));
        }

        public void setOpenAnswerScore(int givenAnswerID, int points)
        {
            _adminRepository.setOpenAnswerScore(givenAnswerID, points);
        }
    }
}