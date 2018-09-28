using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.TestUsers
{
    public class TestLogowanie
    {
        public bool isValidUser(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return true;
            }
            return false;
        }
    }
}