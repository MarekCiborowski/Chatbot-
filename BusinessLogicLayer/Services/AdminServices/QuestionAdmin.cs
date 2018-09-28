using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.IAdmin;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Repositories;

namespace BusinessLogicLayer.Services.AdminServices
{
    public class QuestionAdmin : IQuestionAdmin
    {

        private IAdminRepository _adminRepository;
        private IMapper _mapper;

        public QuestionAdmin(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;

        }
        public Question CreateQuestion(string value, int categoryID, int points, int questionTypeID)
        {
            return new Question()
            {
                value = value,
                categoryID = categoryID,
                points = points,
                questionTypeID = questionTypeID,
            };
        }

        public Question AddAnswersToQuestion(IEnumerable<Answer> answers, Question question)
        {
            
            return _adminRepository.AddAnswersToQuestion(answers,question);
        }

        public QuestionVM GetQuestion(int? questionID)
        {
            return MapQuestionToViewModel(_adminRepository.GetQuestion(questionID)); //_adminRepository.GetQuestion(questionID);
        }

        public IEnumerable<QuestionVM> GetAllQuestions()
        {
            var questions = _adminRepository.GetAllQuestions();
            List<QuestionVM> questionsVM = new List<QuestionVM>();

            foreach(var q in questions)
            {
                questionsVM.Add(MapQuestionToViewModel(q));
            }
            return questionsVM.AsEnumerable();

            //return _adminRepository.GetAllQuestions();
        }

        public void AddQuestion(QuestionVM questionVM)
        {
            Question question = MapQuestionToModel(questionVM);
            _adminRepository.AddQuestion(question);
        }

        public void RemoveQuestion(int? id)
        {
            _adminRepository.RemoveQuestion(id);
        }

        public Question EditQuestion(Question question, IEnumerable<Answer> answers)
        {
            return _adminRepository.EditQuestion(question, answers);
        }

        public void EditQuestion(QuestionVM questionVM)
        {
            Question question = MapQuestionToModel(questionVM);
             _adminRepository.EditQuestion(question);
        }

        public string GetCategoryName(int categoryID)
        {
            return _adminRepository.GetCategoryName(categoryID).categoryName;
        }

        public string GetQuestionType(int questionTypeID)
        {
            return _adminRepository.GetQuestionType(questionTypeID).typeName;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _adminRepository.GetAllCategories();
        }

        public IEnumerable<QuestionType> GetAllQuestionTypes()
        {
            return _adminRepository.GetAllQuestionTypes();
        }

        public IEnumerable<AnswerVM> GetAnswersByQuestionId(int? QuestionId)
        {
            var answers = _adminRepository.GetAnswersByQuestionId(QuestionId);
            List<AnswerVM> answersVM = new List<AnswerVM>();

            foreach(var a in answers)
            {
                answersVM.Add(MapAnswerToViewModel(a));
            }

            return answersVM.AsEnumerable();

        }
                  
        public void AddAnswer(AnswerVM answerVM)
        {
             _adminRepository.AddAnswer(MapAnswerToModel(answerVM));
        }

        public AnswerVM GetAnswer(int? id)
        {
            var answer = _adminRepository.GetAnswer(id);
            return (MapAnswerToViewModel(answer));
        }

        public void DeleteAnswer(int? id)
        {
            _adminRepository.RemoveAnswer(id);
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

        public Answer MapAnswerToModel(AnswerVM answerVM)
        {
            Answer answer = _mapper.Map<Answer>(answerVM);
            return answer;
        }

        public AnswerVM MapAnswerToViewModel(Answer answer)
        {
            AnswerVM answerVM = _mapper.Map<AnswerVM>(answer);
            return answerVM;
        }

    }
}
