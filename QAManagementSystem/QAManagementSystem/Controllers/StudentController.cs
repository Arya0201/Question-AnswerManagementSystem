using QAManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QAManagementSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        static int QPaperId;
        // GET: Student
        private QAManagementSystemEntities db = new QAManagementSystemEntities();
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(@Session["UserId"].ToString());
            List<QuestionPaper> questionPapers = db.QuestionPapers.Where(d => d.Status == "Approved").ToList();
            List<QuestionPaper> finalquestionPapers = new List<QuestionPaper>(); ;
            foreach (var questionpaper in questionPapers)
            {
                bool exists = db.SubmittedQuestionPapers.Any(s => s.QuestionPaperId ==  questionpaper.QuestionPaperId && s.UserId == userId);
                if(!exists)
                {
                    finalquestionPapers.Add(questionpaper);
                }
            }
            return View(finalquestionPapers);
        }

        public ActionResult ShowQuestions(int QuestionPaperId , string Title, string Description)
        {
            ViewBag.QuestionPaperId = QuestionPaperId;
            ViewBag.Title = Title;
            ViewBag.Description = Description;
            QPaperId = QuestionPaperId;
           List<Question> questions = db.Questions.Where(q => q.QuestionPaperId == QuestionPaperId).ToList();

            return View(questions);
        }

        [HttpPost]
        public ActionResult SubmitAnswers(List<Answer> answers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    DateTime submissionTime = DateTime.Now;

                  
                    foreach (var answer in answers)
                    {
                        answer.SubmissionTimestamp = submissionTime;
                        db.Answers.Add(answer);
                    }
                    SubmittedQuestionPaper sa = new SubmittedQuestionPaper();
                    sa.UserId = Convert.ToInt32( @Session["UserId"].ToString());
                    sa.SubmissionTimestamp = submissionTime;
                    sa.QuestionPaperId = QPaperId;

                    db.SubmittedQuestionPapers.Add(sa);

                    db.SaveChanges();

                 
                    return RedirectToAction("SubmittedQuestionPaper","Student");
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError("", "An error occurred while saving the answers. Please try again.");

                    return RedirectToAction("Index");
         
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

         
        }

      public ActionResult  SubmittedQuestionPaper()
        {
           int userId= Convert.ToInt32(@Session["UserId"].ToString());
            var submittedQuestionPapers = (from submission in db.SubmittedQuestionPapers
                                           where submission.UserId == userId
                                           join questionPaper in db.QuestionPapers
                                           on submission.QuestionPaperId equals questionPaper.QuestionPaperId
                                           select new SubmittedQuestionPaperViewModel
                                           {
                                               SubmissionID = submission.SubmissionID,
                                               QuestionPaperID =(int) submission.QuestionPaperId,
                                               SubmissionTimestamp =(DateTime) submission.SubmissionTimestamp,
                                               QuestionPaperTitle = questionPaper.Title,
                                               QuestionPaperDescription = questionPaper.Description,
                                               
                                           }).ToList();
            return View(submittedQuestionPapers);
        }
    }
}