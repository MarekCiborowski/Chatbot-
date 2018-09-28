using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces.IAdmin;
using DataAccessLayer.Models;
using Repositories;

namespace BusinessLogicLayer.Services.AdminServices
{
    public class AnswerAdmin :IAnswerAdmin
    {
        private IAdminRepository _adminRepository;

        public AnswerAdmin(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Answer CreateAnswer(string value, bool isCorrect )
        {
            return new Answer()
            {
                value = value,
                isCorrect = isCorrect
            };
        }

        public Answer GetAnswer(int? answerID)
        {
            return _adminRepository.GetAnswer(answerID);
        }
       

        public void DeleteAnswer(int? id)
        {
            _adminRepository.RemoveAnswer(id);
        }

        

        public Answer EditAnswer(Answer answer)
        {
            return _adminRepository.EditAnswer(answer);

        }
    }
}
