using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAManagementSystem.Models
{
    public class SubmittedQuestionPaperViewModel
    {
        public int SubmissionID { get; set; }
        public int QuestionPaperID { get; set; }
        public DateTime SubmissionTimestamp { get; set; }
        public string QuestionPaperTitle { get; set; }
        public string QuestionPaperDescription { get; set; }
    }
}