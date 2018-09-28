using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.ViewModels;

namespace AdminPanel.ViewModels.AdminViewModels.FinishedTestViewModels
{
    public class FinishedTestDetailsVM
    {
        public TestVM testVM { get; set; }
        public IEnumerable<QuestionAndGivenAnswerVM> questionsAndGivenAnswersVM { get; set; }        
    }
}