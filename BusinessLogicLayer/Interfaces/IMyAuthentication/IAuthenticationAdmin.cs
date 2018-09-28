using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Interfaces.IMyAuthentication
{
    public interface IAuthenticationAdmin
    {
        bool Login(string userName, string password);
        void Logout();
        
    }
}
