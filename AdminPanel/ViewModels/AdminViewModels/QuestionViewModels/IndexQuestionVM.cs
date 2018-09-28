using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;

namespace AdminPanel.ViewModels.AdminViewModels.QuestionViewModels
{
    public class IndexQuestionVM
    {
        public IEnumerable<QuestionVM> question  { get; set; }
        public string category { get; set; }
        public string questionType { get; set; }
    }
}