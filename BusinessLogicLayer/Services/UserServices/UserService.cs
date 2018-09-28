using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces.IUser;
using DataAccessLayer.Models;
using Repositories;

namespace BusinessLogicLayer.Services.UserServices
{
    public class UserService : IUserServices
    {

        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public int? GetTestByKey(string testKey)
        {
            return _userRepository.GetTestByKey(testKey);
        }

        public IEnumerable<Question> GetQuestionsByTestId(int id)
        {
            return _userRepository.GetQuestionsByTestId(id);
        }


        public GivenAnswer CreateGivenAnswer(int testID, int questionID, string answer)
        {
            return _userRepository.CreateGivenAnswer(testID, questionID, answer);
        }

        public void AddGivenAnswer(GivenAnswer givenAnswer)
        {
            _userRepository.AddGivenAnswer(givenAnswer);
        }

        public GivenAnswer EditGivenAnswer(GivenAnswer givenAnswer, string newAnswer)
        {
            return _userRepository.EditGivenAnswer(givenAnswer, newAnswer);
        }


        public void FinalizingTest(int testID)//, TimeSpan completionTime)
        {
            _userRepository.FinalizingTest(testID);//, completionTime);
        }

        public IEnumerable<Answer> GetAnswersForQuestion(int questionID)
        {
            return _userRepository.GetAnswersForQuestion(questionID);
        }
        public QuestionType GetQuestionType(int questionTypeID)
        {
            return _userRepository.GetQuestionType(questionTypeID);
        }

        public int GetQuestionTypeID(int questionID)
        {
            return _userRepository.GetQuestionTypeID(questionID);
        }


    }
}
