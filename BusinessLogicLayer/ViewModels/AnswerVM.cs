using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class AnswerVM
    {
        public int answerID { get; set; }
        public string value { get; set; }
        public bool isCorrect { get; set; }        
        public int questionID { get; set; }        
    }
}
