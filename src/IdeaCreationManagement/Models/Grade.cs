using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int UsefulnessValue { get; set; }
        public int DifficultyValue { get; set; }
        public int Ingenuity { get; set; }
        [ForeignKey("Rater")]
        public string RaterId { get; set; }
        public User Rater { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}