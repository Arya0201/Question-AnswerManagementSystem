using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAManagementSystem.Models
{
    public class QuestionPaperWithSubmittedQuestionsViewModel
    {
        public int QuestionPaperId { get; set; }
        public List<QuestionWithSubmittedAnswerViewModel> QuestionsWithSubmittedAnswers { get; set; }
    }
    public class QuestionWithSubmittedAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
        public string SubmittedAnswer { get; set; }
    }
}