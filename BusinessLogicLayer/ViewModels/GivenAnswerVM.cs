using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class GivenAnswerVM
    {
        public int givenAnswerID { get; set; }
        public string answer { get; set; } 
        public bool isCorrect { get; set; }
        public int points { get; set; }       
        public int testID { get; set; } 
        public int questionID { get; set; }        
    }
}
