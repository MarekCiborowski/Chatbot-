using BusinessLogicLayer.Interfaces.IAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPanel.ViewModels.AdminViewModels.TestSchemeViewModels;
using DataAccessLayer.Models;
using System.Data.Entity;
using AdminPanel.ViewModels.AdminViewModels.TestViewModels;

namespace AdminPanel.Controllers
{
  //  [Authorize]
    public class TestSchemeController : Controller
    {
        private ITestSchemeAdmin testSchemeService;

        public TestSchemeController(ITestSchemeAdmin _TestSchemeAdmin)
        {
           testSchemeService = _TestSchemeAdmin;
        }

        // GET: TestScheme
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTestScheme()
        {
            List<questionInfo> questions = new List<questionInfo>();
            List<Question> List = new List<Question>();
            List = testSchemeService.GetAllQuestions().ToList();
            foreach (Question q in List)
            {
                questions.Add(new questionInfo { questionID = q.questionID, Text = q.value, Type = testSchemeService.GetQuestionTypeName(q.questionTypeID), isChecked=false });
            }
            
            return View(new CreateTestSchemeVM { questionList = questions});
        }

        [HttpPost]
        public ActionResult CreateTestScheme(CreateTestSchemeVM testVM)
        {
            if (ModelState.IsValid)
            {
                List<int> questionsIDs = new List<int>();
                foreach (questionInfo question in testVM.questionList)
                {
                    if (question.isChecked == true)
                        questionsIDs.Add(question.questionID);
                    
                }
                TestTemplate testTemplate = testSchemeService.CreateTestScheme(testVM.testSchemeName, testVM.testSchemeDescription, questionsIDs);
                testSchemeService.AddTestScheme(testTemplate);
                testSchemeService.AddTemplateQuestions(testSchemeService.GetQuestionListFromID(questionsIDs), testTemplate);
                
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult EditTestScheme(int id)
        {
            TestTemplate testTemplate = new TestTemplate();
            testTemplate = testSchemeService.GetTestTemplate(id);
            return View();
        }

        public ActionResult DeleteTestScheme()
        {
            List<TestTemplate> testTemplates = new List<TestTemplate>();
            List<templateInfo> schemaIDsList = new List<templateInfo>();
            testTemplates = testSchemeService.GetTestTemplates().ToList();
            foreach (TestTemplate t in testTemplates)
            {
                if (t.templateName == null) t.templateName = "Unnamed templated";
                if (t.description == null) t.description = "No description";
                schemaIDsList.Add(new templateInfo(t.testTemplateID, t.templateName, t.description, false));
            }
            return View(new DeleteTestSchemeVM(schemaIDsList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTestScheme(DeleteTestSchemeVM testVM)
        {
            if (ModelState.IsValid)
            {
                foreach (templateInfo info in testVM.schemaList)
                {
                    if (info.isChecked == true)
                        testSchemeService.DeleteTestScheme(info.templateID);
                }
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DetailsTestScheme(int? id)
        {
            return View();
        }
    }
}