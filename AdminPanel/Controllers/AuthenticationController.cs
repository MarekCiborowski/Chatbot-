using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdminPanel.TestUsers;
using BusinessLogicLayer.Interfaces.IMyAuthentication;
using AdminPanel.ViewModels.AuthenticationViewModels;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace AdminPanel.Controllers
{
    public class AuthenticationController : Controller
    {
        public IAuthenticationAdmin authenticationService;
        public AuthenticationController(IAuthenticationAdmin myAuthentication)
        {
           authenticationService = myAuthentication;
        }

        public ActionResult Login()
        {
            //_myAuthentication.Login();
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVM loginVM)
        {
            TestLogowanie testLog = new TestLogowanie();
            if (testLog.isValidUser(loginVM.userName, loginVM.password))
            {
                FormsAuthentication.SetAuthCookie(loginVM.userName, false);
                return RedirectToAction("Index", "Admin");
            }
            ModelState.AddModelError("CredentialError", "Niepoprawna nazwa użytkownika lub hasło");
            return View("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }


       /* public ActionResult NewUser()
        {
            ViewBag.availability = true;

            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UserVM newAdmin)
        {
            return View(newAdmin);
        }*/









            /*

        public void TwojaStara()
        {
            using (var context = new DB())
            {
                var cat = new Category() { categoryName = "category" };
                context.categories.Add(cat);
                context.SaveChanges();
            }
        }
        */
    }
}