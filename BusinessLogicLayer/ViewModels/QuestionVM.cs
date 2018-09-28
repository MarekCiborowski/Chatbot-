using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class QuestionVM
    {
        public int questionID { get; set; }
        public string value { get; set; }
        public int points { get; set; }        
        public int categoryID { get; set; }       
        public int questionTypeID { get; set; }        
    }
}
