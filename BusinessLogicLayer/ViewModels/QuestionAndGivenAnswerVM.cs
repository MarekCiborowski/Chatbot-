using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.ViewModels;

namespace BusinessLogicLayer.ViewModels
{
    public class QuestionAndGivenAnswerVM
    {
        public QuestionVM questionVM { get; set; }
        public IEnumerable<GivenAnswerVM> givenAnswerVM { get; set; }        
    }
}



