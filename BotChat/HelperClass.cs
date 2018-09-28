using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotChat
{
    public class HelperClass
    {
        public int questionID { get; set; }
        public string answer { get; set; }
        public HelperClass(int _questionID, string _answer)
        {
            questionID = _questionID;
            answer = _answer;
        }
    }
}