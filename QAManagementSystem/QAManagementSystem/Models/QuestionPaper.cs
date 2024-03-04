//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QAManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionPaper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionPaper()
        {
            this.Questions = new HashSet<Question>();
            this.SubmittedQuestionPapers = new HashSet<SubmittedQuestionPaper>();
        }
    
        public int QuestionPaperId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreatorUserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubmittedQuestionPaper> SubmittedQuestionPapers { get; set; }
    }
}