using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.ViewModels.AdminViewModels.FinishedTestViewModels;
using BusinessLogicLayer.Interfaces.IAdmin;
using BusinessLogicLayer.ViewModels;

namespace AdminPanel.Controllers
{
    public class FinishedTestController : Controller
    {
        private IQuestionAdmin _questionService;
        private ITestAdmin _testService;
        private IDropDownListHelper _dropDownListHelper;

        public FinishedTestController(IQuestionAdmin iQuestionAdmin, ITestAdmin testAdmin, IDropDownListHelper dropDownListHelper)
        {
            _questionService = iQuestionAdmin;
            _testService = testAdmin;
            _dropDownListHelper = dropDownListHelper;
        }

        // GET: FinishedTest
        public ActionResult Index(string searchString)
        {
            var tests = _testService.GetAllFinishedTests();

            if (!String.IsNullOrEmpty(searchString))
            {
                tests = tests.Where(s => s.username.Contains(searchString));
            }

            return View(tests.ToList());
        }
                
        public ActionResult Details(int? testID)
        {
            if (testID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FinishedTestDetailsVM vm = new FinishedTestDetailsVM
            {
                testVM = _testService.GetTestVM(testID)
            };

            List<QuestionAndGivenAnswerVM> questionsAndGivenAnswerVMList = new List<QuestionAndGivenAnswerVM>();

            var questions = _testService.GetQuestionsByTestId(testID);

            foreach (var q in questions)
            {
                QuestionAndGivenAnswerVM questionAndGivenAnswerVM = new QuestionAndGivenAnswerVM
                {
                    questionVM = q,
                    givenAnswerVM = _testService.GetGivenAnswers(testID, q.questionID)
                };

                questionsAndGivenAnswerVMList.Add(questionAndGivenAnswerVM);
            }
            vm.questionsAndGivenAnswersVM = questionsAndGivenAnswerVMList;

            #region ViewBagData

            ViewBag.pointsList1max = _dropDownListHelper.GetNumericDropDownList(1);
            ViewBag.pointsList2max = _dropDownListHelper.GetNumericDropDownList(2);
            ViewBag.pointsList3max = _dropDownListHelper.GetNumericDropDownList(3);
            ViewBag.pointsList4max = _dropDownListHelper.GetNumericDropDownList(4);
            ViewBag.pointsList5max = _dropDownListHelper.GetNumericDropDownList(5);
            ViewBag.pointsList6max = _dropDownListHelper.GetNumericDropDownList(6);
            ViewBag.pointsList7max = _dropDownListHelper.GetNumericDropDownList(7);
            ViewBag.pointsList8max = _dropDownListHelper.GetNumericDropDownList(8);
            ViewBag.pointsList9max = _dropDownListHelper.GetNumericDropDownList(9);
            ViewBag.pointsList10max = _dropDownListHelper.GetNumericDropDownList(10);

            #endregion

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int points, int givenAnswerID, int testID_)
        {
            if (ModelState.IsValid)
            {
               _testService.setOpenAnswerScore(givenAnswerID, points);
            }

            return RedirectToAction("Details", new { testID = testID_ });
        }
    }
}