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
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            // Retrieve username, email, and role from session
            string username = Session["Username"] as string;
            string email = Session["Email"] as string;
            string role = Session["Role"] as string;

            // Retrieve username, email, and role from session
            ViewBag.Username = Session["Username"] as string;
            ViewBag.Email = Session["Email"] as string;
            ViewBag.Role = Session["Role"] as string;

            return View();
        }
        public ActionResult GetQuestionPaper()
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            // Retrieve question papers associated with the specified user ID
            var questionPapersForUser = db.QuestionPapers.Where(q => q.CreatorUserId == userId && q.Status=="Draft").ToList();

            // Pass the filtered question papers to the view
            return View(questionPapersForUser);
        }
        public ActionResult AddQuestion(int QuestionPaperId, string Title, string Description)
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }


            ViewBag.QPtitle = Title;
            ViewBag.QPId = QuestionPaperId;
            ViewBag.QPDescription = Description;
            Session["QuestionPaperId"] = QuestionPaperId;
            Session["Title"] = Title;
            Session["Description"] = Description;


            List<Question> questions = db.Questions.Where(q => q.QuestionPaperId == QuestionPaperId).ToList();


            return View(questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question model, int id)
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
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
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
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
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            QuestionPaper qp = db.QuestionPapers.Find(questionPaperId);
            qp.Status = "Pending";

            db.SaveChanges();
            return RedirectToAction("PendingQuestionPaperList", "Teacher");
        }

        public ActionResult PendingQuestionPaperList()
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
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
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(q => q.CreatorUserId == userId && q.Status == "Rejected").ToList();
            return View(questionPapers);
        }

        public ActionResult EditRejectedQuestionPaper(int questionPaperId)
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            QuestionPaper questionPaper = db.QuestionPapers.Find(questionPaperId);
            questionPaper.Status = "Draft";

            db.SaveChanges();

            return RedirectToAction("RejectedQuestionPaper");
        }

        public ActionResult DeleteRejectedQuestionPaper(int questionPaperId)
        {
            // Check if the user is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Teacher")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
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