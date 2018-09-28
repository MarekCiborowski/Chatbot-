using BusinessLogicLayer.Interfaces.IAdmin;
using AdminPanel.ViewModels.AdminViewModels.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;

namespace AdminPanel.Controllers
{
   // [Authorize]
    public class TestController : Controller
    {
        private ITestAdmin testService;

        public TestController(ITestAdmin iTestAdmin)
        {
            testService = iTestAdmin;
        }

        // GET: Test
        public ActionResult Index()
        {
            var tests = testService.GetAllTestsVM().ToList();

            return View(tests);
        }

        public ActionResult ShowTest(int? id)
        {
            return View();
        }

        public ActionResult ShowTestResult(int? id)
        {
            return View();
        }

        public ActionResult CreateTest()
        {
            List<templateInfo> schemaIDsList = new List<templateInfo>();
            List<TestTemplate> testTemplates = new List<TestTemplate>();
            testTemplates = testService.GetTestTemplates().ToList();
            foreach (TestTemplate t in testTemplates)
            {
                if (t.templateName == null) t.templateName = "Unnamed templated";
                if (t.description == null) t.description = "No description";
                schemaIDsList.Add(new templateInfo(t.testTemplateID, t.templateName, t.description));
            }

            CreateTestVM vm = new CreateTestVM(schemaIDsList);
            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTest(CreateTestVM testVM)
        {
            int templateID=-1;

            templateID = testVM.selectedTemplateID;
            string key = testService.RandomString();
            Test newTest = testService.CreateTest(templateID, testVM.userName, key, testVM.timeLimit.Days*24*60+testVM.timeLimit.Hours*60+testVM.timeLimit.Minutes, testVM.Deadline);
            testService.AddTest(newTest);

            return View("ShowKey", new ShowKeyVM(key));
        }

        public ActionResult DeleteTest()
        {
            List<testInfo> tests = new List<testInfo>();
            foreach(Test test in testService.GetAllTests())
            {
                tests.Add(new testInfo(test.testID, test.username, testService.GetTestTemplate(test.testTemplateID).templateName, test.isCompleted, test.expirationDate, false));
            }

            return View(new DeleteTestVM(tests));
        }

        [HttpPost, ActionName("DeleteTest")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTestConfirmed(DeleteTestVM testVM)
        {
            foreach(var element in testVM.testList)
            {
                if (element.isChecked == true)
                    testService.DeleteTest(element.testID);
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DetailsTest(int? id)
        {
            return View();
        }
    }
}