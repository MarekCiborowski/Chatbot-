using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;

namespace AdminPanel.ViewModels.AdminViewModels.QuestionViewModels
{
    public class EditQuestionVM
    {
        // [Display(Name = "Treść pytania")]
        //[Required(ErrorMessage = "Wymagana treść pytania")]
        public QuestionVM question { get; set; }

        //public string SelectedQuestionType { get; set; }
        public IEnumerable<SelectListItem> questionTypes { get; set; }

        //public string SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        // public UserVM author { get; set; }
    }
}