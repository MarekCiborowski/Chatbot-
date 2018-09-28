using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;

namespace AdminPanel.ViewModels.AdminViewModels.QuestionViewModels
{
    public class DetailsQuestionVM
    {
        public QuestionVM question { get; set; }
        public Category category { get; set; }
        public QuestionType questionType { get; set; }

        public AnswerVM newAnswer { get; set; }
        public IEnumerable<AnswerVM> answers { get; set; }
    }
}