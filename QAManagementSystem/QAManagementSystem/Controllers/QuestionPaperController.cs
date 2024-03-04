using QAManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QAManagementSystem.Controllers
{
    public class QuestionPaperController : Controller
    {

        private QAManagementSystemEntities db = new QAManagementSystemEntities();
        // GET: QuestionPaper
        

        public ActionResult CreateQuestionPaper()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestionPaper(QuestionPaper model)
        {
            model.CreationDate = DateTime.Now;
            if(Session["Role"].ToString() == "Admin")
            {
                model.Status = "Approved";
            }
            else
            {
                model.Status = "Draft";
            }

            model.CreatorUserId = Convert.ToInt32( Session["UserId"]);

            db.QuestionPapers.Add(model);
            db.SaveChanges();

            return RedirectToAction("GetQuestionPaper",Session["Role"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestionPaper(int questionPaperId)
        {
            try
            {
            // Find the question paper in the database
            var questionPaper = db.QuestionPapers.Find(questionPaperId);
            var questions = db.Questions.Where(q => q.QuestionPaperId == questionPaperId).ToList();

            // Remove each question found
            db.Questions.RemoveRange(questions);
            db.SaveChanges();


                // Remove the question paper from the database
                db.QuestionPapers.Remove(questionPaper);
                db.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("GetQuestionPaper", "Teacher"); // Redirect to the index page of question papers
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index"); // Redirect to the index page of question papers
            }
        }

        public ActionResult DetailsQuestion(int QuestionPaperId,string Title,string Description)
        {
            ViewBag.QuestionPaperId = QuestionPaperId;
            ViewBag.Title = Title;
            ViewBag.Description = Description;

            List<Question> questions = db.Questions.Where(q => q.QuestionPaperId == QuestionPaperId).ToList();
            return View(questions);
        }

    }
}