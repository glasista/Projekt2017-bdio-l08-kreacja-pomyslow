using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public DateTime TimeOfChange { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        [ForeignKey("AuthorOfChange")]
        public string AuthorOfChangeId { get; set; }
        public User AuthorOfChange { get; set; }
        public bool StudentRead { get; set; }
        public bool EmployeeRead { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}