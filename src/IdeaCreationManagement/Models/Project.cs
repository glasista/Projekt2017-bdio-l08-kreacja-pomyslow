using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public User Author { get; set; }
        [ForeignKey("Assignee")]
        public string AssigneeId { get; set; }
        public User Assignee { get; set; }
        public ProjectType Type { get; set; }
        public float AverageGrade { get; set; }
        public float AverageUsefulness { get; set; }
        public float AverageDifficulty { get; set; }
        public float AverageIngenuity { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}