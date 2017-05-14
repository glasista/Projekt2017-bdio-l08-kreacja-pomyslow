using System;

namespace IdeaCreationManagement.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public DateTime Time { get; set; }
        public int UsefulnessValue { get; set; }
        public int DifficultyValue { get; set; }
        public int Ingenuity { get; set; }
        public int RaterId { get; set; }
        public User Rater { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}