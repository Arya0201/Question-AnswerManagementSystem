using QAManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QAManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    public class QuestionPaperController : Controller
    {

        private QAManagementSystemEntities db = new QAManagementSystemEntities();
        // GET: QuestionPaper
        

        public ActionResult CreateQuestionPaper()
        {   // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestionPaper(QuestionPaper model)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

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
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
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
                return RedirectToAction("GetQuestionPaper", Session["Role"].ToString()); // Redirect to the index page of question papers

            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during deletion
                // You may want to log the error or show an error message to the user
                return RedirectToAction("GetQuestionPaper", Session["Role"].ToString()); // Redirect to the index page of question papers
            }
        }

        public ActionResult DetailsQuestion(int QuestionPaperId,string Title,string Description)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            ViewBag.QuestionPaperId = QuestionPaperId;
            ViewBag.Title = Title;
            ViewBag.Description = Description;

            List<Question> questions = db.Questions.Where(q => q.QuestionPaperId == QuestionPaperId).ToList();
            return View(questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuetionView(int QuestionId)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            var question = db.Questions.Find(QuestionId);

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question model)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            try
            {
                
                    // Retrieve the existing question from the database
                    var existingQuestion = db.Questions.FirstOrDefault(q => q.QuestionId == model.QuestionId);

                  
                        // Update the existing question with the data from the model
                        existingQuestion.QuestionText = model.QuestionText;
                        existingQuestion.OptionA = model.OptionA;
                        existingQuestion.OptionB = model.OptionB;
                        existingQuestion.OptionC = model.OptionC;
                        existingQuestion.OptionD = model.OptionD;
                        existingQuestion.CorrectAnswer = model.CorrectAnswer;

                    // Save changes to the database
                    db.SaveChanges();          
               
                   
                return RedirectToAction("AddQuestion", Session["Role"].ToString(), new { QuestionPaperId = Session["QuestionPaperId"].ToString(), Title = Session["Title"].ToString(), Description = Session["Description"].ToString() });

            }
            catch (Exception ex)
            {
                return View("EditQuestionView", model.QuestionId);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowFullQuestionWithAnswer(int questionPaperId)
        {
            // Check if the user  is not logged in
            if ((Session["Username"] == null || Session["Email"] == null || Session["Role"] == null))
            {
                // If not logged in, redirect to the login page or handle the scenario as needed
                return RedirectToAction("Login", "Account"); // Assuming "Login" action is in "Account" controller
            }

            var userId = Session["UserId"].ToString();
            var questionPaper = db.QuestionPapers.Find(questionPaperId);

            var questionsWithSubmittedAnswers = db.Questions
                .Where(q => q.QuestionPaperId == questionPaperId)
                .Select(q => new QuestionWithSubmittedAnswerViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    CorrectAnswer = q.CorrectAnswer,
                    SubmittedAnswer = db.Answers
                        .Where(a => a.UserId.ToString() == userId && a.QuestionId == q.QuestionId)
                        .Select(a => a.AnswerText)
                        .FirstOrDefault()
                })
                .ToList();

            var viewModel = new QuestionPaperWithSubmittedQuestionsViewModel
            {
                QuestionPaperId = questionPaperId,
                QuestionsWithSubmittedAnswers = questionsWithSubmittedAnswers
            };

            var viewModelList = new List<QuestionPaperWithSubmittedQuestionsViewModel> { viewModel };
            return View(viewModelList);



        }


    }
}