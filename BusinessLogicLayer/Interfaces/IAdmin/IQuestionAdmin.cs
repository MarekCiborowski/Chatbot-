using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.IAdmin
{
    public interface IQuestionAdmin
    {
        Question CreateQuestion(string value, int categoryID, int points, int questionTypeID);

        void EditQuestion(QuestionVM question);

        Question AddAnswersToQuestion(IEnumerable<Answer> answers, Question question);

        AnswerVM GetAnswer(int? id);
        void DeleteAnswer(int? id);
        QuestionVM GetQuestion(int? questionID);

        IEnumerable<QuestionVM> GetAllQuestions();

        void AddQuestion(QuestionVM question);

        void AddAnswer(AnswerVM answerVM);

        void RemoveQuestion(int? id);

        IEnumerable<AnswerVM> GetAnswersByQuestionId(int? QuestionId);

        Question EditQuestion(Question question, IEnumerable<Answer> answers);
        string GetCategoryName(int categoryID);
        string GetQuestionType(int questionTypeID);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<QuestionType> GetAllQuestionTypes();

    }
}
