using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QAManagementSystem.Models;

namespace QAManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private QAManagementSystemEntities db = new QAManagementSystemEntities();
        // GET: Admin
        public ActionResult Index()
        {   
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null ) && Session["Role"]!="Admin")
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

        public ActionResult CreateUser()
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            return View();
        }

        public ActionResult UserList()
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            // Retrieve the list of users from the database
            List<User> userList = db.Users.ToList();

            // Pass the list of users to the view
            return View(userList);
        }

        [HttpPost]
        public ActionResult UserList(string Username, string Email,string Password,string Role)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            User u = new User();
            u.Username = Username;
            u.Password = Password;
            u.Role = Role;
            u.Email = Email;

            db.Users.Add(u);
            db.SaveChanges();

            return RedirectToAction("UserList");
        }

        public ActionResult EditUser(int userId)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            User user = db.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Roles = new SelectList(new List<string> { "Admin", "Teacher", "Student" });

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User model)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            if (ModelState.IsValid)
            {
                // Find the user by UserId
                User userToUpdate = db.Users.Find(model.UserId);

                if (userToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Update the user's information with the values from the form
                userToUpdate.Username = model.Username;
                userToUpdate.Password = model.Password; // Note: You should handle password securely
                userToUpdate.Email = model.Email;
                userToUpdate.Role = model.Role;

                // Save the changes to the database
                db.SaveChanges();

                return RedirectToAction("UserList", "Admin");
            }

            // If ModelState is not valid, redisplay the form with validation errors
            return View(model);
        }

        public ActionResult DeleteUser(int userId)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            User user=  db.Users.Find(userId);

            if(user!=null)
            {
                db.Users.Remove(user);
                db.SaveChanges();

            }
            else
            {
                ViewData["ErrorMessage"] = "Failed to delete user.";
            }
                return RedirectToAction("UserList");
        }


        public ActionResult GetQuestionPaper()
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            // Retrieve question papers associated with the specified user ID
            var questionPapersForUser = db.QuestionPapers.Where(q => q.CreatorUserId == userId).ToList();

            // Pass the filtered question papers to the view
            return View(questionPapersForUser);
        }

        public ActionResult AddQuestion(int QuestionPaperId, string Title, string Description)
        {

            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
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
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Map the view model to a new Question entity
                Question question = new Question
                {
                    QuestionPaperId = id,
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

            QuestionPaper qp = db.QuestionPapers.Find(id);

            return RedirectToAction("AddQuestion", "Admin", new { QuestionPaperId = id, Title = qp.Title, Description = qp.Description });


        }

        public ActionResult PendingQuestionPaperList()
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            int userID =Convert.ToInt32( Session["UserId"].ToString());
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(q=> q.CreatorUserId!=userID &&  (q.Status == "Pending")).ToList();
            return View(questionPapers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestionPaper(int questionPaperId)
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
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
                return RedirectToAction("PendingQuestionPaperList", "Admin"); // Redirect to the index page of question papers
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index"); // Redirect to the index page of question papers
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int questionId, int questionPaperId, string Title, string Description)
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
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
                return RedirectToAction("AddQuestion", "Admin", new { QuestionPaperId = questionPaperId, Title = Title, Description = Description });
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index");
            }
        }


        public ActionResult ApproveQuestionPaper( int questionPaperId)
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            try
            {
                // Find the question paper in the database
                var questionPaper = db.QuestionPapers.Find(questionPaperId);


                questionPaper.Status = "Approved";
                db.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("PendingQuestionPaperList", "Admin"); // Redirect to the index page of question papers
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("Index"); // Redirect to the index page of question papers
            }


        }
        public ActionResult RejectQuestionPaper(int questionPaperId)
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            try
            {
                // Find the question paper in the database
                var questionPaper = db.QuestionPapers.Find(questionPaperId);


                questionPaper.Status = "Rejected";
                db.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("PendingQuestionPaperList", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index"); //
            }
        }

            public ActionResult ApprovedQuestionPaper()
        {
           List<QuestionPaper> questionPapers =  db.QuestionPapers.Where(q => q.Status == "Approved").ToList();

            return View(questionPapers);
        }

        public ActionResult DeleteApprovedQuestionPaper(int questionPaperId)
        {
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null) && Session["Role"] != "Admin")
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            try
            {
                // Find the question paper in the database
                var questionPaper = db.QuestionPapers.Find(questionPaperId);
                var questions = db.Questions.Where(q => q.QuestionPaperId == questionPaperId).ToList();
                var submittedQuestioinPapers = db.SubmittedQuestionPapers.Where(q => q.QuestionPaperId == questionPaperId).ToList();


                foreach (var question in questions)
                {
                    // Find answers associated with the current question
                    var answers = db.Answers.Where(a => a.QuestionId == question.QuestionId).ToList();

                    // Remove each answer found
                    db.Answers.RemoveRange(answers);
                    db.SaveChanges();

                    // Remove the question itself
                    db.Questions.Remove(question);

                }

                //remove the submitted questionpapers
                db.SubmittedQuestionPapers.RemoveRange(submittedQuestioinPapers);

                // Remove the question paper from the database
                db.QuestionPapers.Remove(questionPaper);
                db.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("ApprovedQuestionPaper", "Admin"); // Redirect to the index page of question papers
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