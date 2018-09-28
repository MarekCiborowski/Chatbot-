using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogicLayer.Interfaces.IMyAuthentication;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.AuthenticationService
{
    public class AuthenticationAdmin  : IAuthenticationAdmin
    {
        public bool Login(string userName, string password)
        {
            if (IsValidUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return true;
            }
            return false;
        }

        private bool IsValidUser(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return true;
            }
            return false;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
