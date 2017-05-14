using System;

namespace IdeaCreationManagement.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public DateTime TimeOfChange { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int AuthorOfChangeId { get; set; }
        public User AuthorOfChange { get; set; }
        public bool StudentRead { get; set; }
        public bool EmployeeRead { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}