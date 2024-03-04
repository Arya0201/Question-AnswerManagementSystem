using QAManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QAManagementSystem.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {

        private QAManagementSystemEntities db = new QAManagementSystemEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetQuestionPaper()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            // Retrieve question papers associated with the specified user ID
            var questionPapersForUser = db.QuestionPapers.Where(q => q.CreatorUserId == userId && q.Status=="Draft").ToList();

            // Pass the filtered question papers to the view
            return View(questionPapersForUser);
        }
        public ActionResult AddQuestion(int QuestionPaperId, string Title, string Description)
        {
           

            ViewBag.QPtitle = Title;
            ViewBag.QPId = QuestionPaperId;
            ViewBag.QPDescription = Description;
            
            List<Question> questions = db.Questions.Where(q => q.QuestionPaperId == QuestionPaperId).ToList();


            return View(questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question model, int id)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Map the view model to a new Question entity
                Question question = new Question
                {  QuestionPaperId = id,
                    QuestionText = model.QuestionText,
                    OptionA = model.OptionA,
                    OptionB = model.OptionB,
                    OptionC = model.OptionC,
                    OptionD = model.OptionD,
                    CorrectAnswer = model.CorrectAnswer
                };

                // Add the new question to the database
                db.Questions.Add(question);
                db.SaveChanges();

                // Redirect to a success page or perform other actions
            }

              QuestionPaper qp= db.QuestionPapers.Find(id);

            return RedirectToAction("AddQuestion", "Teacher", new { QuestionPaperId = id, Title=qp.Title, Description =qp.Description});

        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int questionId, int questionPaperId,string Title,string Description)
        {
            // Find the question in the database
            var question = db.Questions.Find(questionId);
                        

            // Check if the question exists
            if (question == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Remove the question from the database
                db.Questions.Remove(question);
                db.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("AddQuestion", "Teacher", new { QuestionPaperId = questionPaperId, Title = Title, Description = Description });
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult GetApproval(int questionPaperId)
        {
            QuestionPaper qp = db.QuestionPapers.Find(questionPaperId);
            qp.Status = "Pending";

            db.SaveChanges();
            return RedirectToAction("PendingQuestionPaperList", "Teacher");
        }

        public ActionResult PendingQuestionPaperList()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(q =>q.CreatorUserId == userId &&  q.Status == "Pending").ToList();
            return View(questionPapers);
        }

        public ActionResult ApprovedQuestionPaper()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(q => q.CreatorUserId == userId && q.Status == "Approved").ToList();
            return View(questionPapers);
        }


        public ActionResult RejectedQuestionPaper()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(q => q.CreatorUserId == userId && q.Status == "Rejected").ToList();
            return View(questionPapers);
        }

        public ActionResult EditRejectedQuestionPaper(int questionPaperId)
        {
            QuestionPaper questionPaper = db.QuestionPapers.Find(questionPaperId);
            questionPaper.Status = "Draft";

            db.SaveChanges();

            return RedirectToAction("RejectedQuestionPaper");
        }

        public ActionResult DeleteRejectedQuestionPaper(int questionPaperId)
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
                return RedirectToAction("RejectedQuestionPaper"); // Redirect to the index page of question papers
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index"); // Redirect to the index page of question papers
            }
        }
    }
}