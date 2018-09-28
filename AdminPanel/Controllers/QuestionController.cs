using BusinessLogicLayer.Interfaces.IAdmin;
using AdminPanel.ViewModels.AdminViewModels.QuestionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using BusinessLogicLayer.Services.AdminServices;
using System.Net;
using BusinessLogicLayer.ViewModels;

namespace AdminPanel.Controllers
{
    //[Authorize]
    public class QuestionController : Controller
    {      

        private IQuestionAdmin questionService;
        

        public QuestionController(IQuestionAdmin iQuestionAdmin)
        {
            questionService = iQuestionAdmin;            
        }
        // GET: Question
        public ActionResult Index()
        {
            return View(questionService.GetAllQuestions().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsQuestion(DetailsQuestionVM vm)
        {
            if (ModelState.IsValid)
            {

                AnswerVM answerToAdd = new AnswerVM();
                answerToAdd.isCorrect = vm.newAnswer.isCorrect;
                answerToAdd.questionID = vm.question.questionID;
                answerToAdd.value = vm.newAnswer.value;


                questionService.AddAnswer(answerToAdd);

                //vm.newAnswer.value = "";
                //vm.newAnswer.isCorrect = false;
                               
                return RedirectToAction("DetailsQuestion", vm.question.questionID);
            }

            return RedirectToAction("DetailsQuestion", vm.question.questionID);
        }

        public ActionResult CreateQuestion()
        {

            // kulturalnie trzeba to zrobić a nie w kontrolerze 

            CreateQuestionVM vm = new CreateQuestionVM();

            var categories = questionService.GetAllCategories();
            var qTypes = questionService.GetAllQuestionTypes();

            vm.Categories = categories.Select(d => new SelectListItem
            {
                Value = d.categoryID.ToString(),
                Text = d.categoryName
            });

            vm.questionTypes = qTypes.Select(d => new SelectListItem
            {
                Value = d.questionTypeID.ToString(),
                Text = d.typeName
            });

            return View(vm);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(CreateQuestionVM questionVM)
        {
            if (ModelState.IsValid)
            {
                QuestionVM newQuestion = questionVM.question;
                questionService.AddQuestion(newQuestion);
                                
                return RedirectToAction("Index", questionService.GetAllQuestions().ToList());
            }
            return View(questionVM);
        }

        public ActionResult EditQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionVM question = questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            EditQuestionVM questionToEdit = new EditQuestionVM();
            questionToEdit.question = question;

            var categories = questionService.GetAllCategories();
            var qTypes = questionService.GetAllQuestionTypes();

            questionToEdit.Categories = categories.Select(d => new SelectListItem
            {
                Value = d.categoryID.ToString(),
                Text = d.categoryName
            });

            questionToEdit.questionTypes = qTypes.Select(d => new SelectListItem
            {
                Value = d.questionTypeID.ToString(),
                Text = d.typeName
            });

            return View(questionToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(EditQuestionVM adminVM)
        {
            if (ModelState.IsValid)
            {                
                questionService.EditQuestion(adminVM.question);

                return RedirectToAction("Index", questionService.GetAllQuestions().ToList());
            }

            return RedirectToAction("Index", questionService.GetAllQuestions().ToList());
            
        }

        public ActionResult DetailsQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionVM question = questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            DetailsQuestionVM questionToShowWithDetails = new DetailsQuestionVM();
            questionToShowWithDetails.question = question;

            var answers = questionService.GetAnswersByQuestionId(id);

            questionToShowWithDetails.answers = answers;

            return View(questionToShowWithDetails);
        }

        public ActionResult DeleteQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionVM question = questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            DeleteQuestionVM questionToDelete = new DeleteQuestionVM();
            questionToDelete.question = question;
            return View(questionToDelete);
        }

        public ActionResult DeleteAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AnswerVM answer = questionService.GetAnswer(id);
            if (answer == null)
            {
                return HttpNotFound();
            }        
            
            questionService.DeleteAnswer(answer.answerID);

            var question = questionService.GetQuestion(answer.questionID);

            return RedirectToAction("DetailsQuestion", new { id = question.questionID });
        }

        [HttpPost, ActionName("DeleteQuestion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestionConfirmed(int id)
        {
            QuestionVM question = questionService.GetQuestion(id);
            questionService.RemoveQuestion(id);
            return RedirectToAction("Index");
        }


    }
}