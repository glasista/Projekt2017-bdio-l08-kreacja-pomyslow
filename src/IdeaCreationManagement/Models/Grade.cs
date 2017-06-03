using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        public int UsefulnessValue { get; set; }
        public int DifficultyValue { get; set; }
        public int Ingenuity { get; set; }
        public int AverageGrade { get; set; }
        [ForeignKey("Rater")]
        public string RaterId { get; set; }
        public User Rater { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}