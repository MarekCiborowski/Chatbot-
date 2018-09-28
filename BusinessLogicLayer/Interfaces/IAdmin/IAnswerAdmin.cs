using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interfaces.IAdmin
{
    public interface IAnswerAdmin
    {
        Answer CreateAnswer(string value, bool isCorrect);


        Answer GetAnswer(int? answerID);



        void DeleteAnswer(int? id);




        Answer EditAnswer(Answer answer);
        
    }
}
