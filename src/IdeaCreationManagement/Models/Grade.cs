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

        [Display(Name = "Przydatność")]
        public int UsefulnessValue { get; set; }

        [Display(Name = "Poziom trudności")]
        public int DifficultyValue { get; set; }

        [Display(Name = "Pomysłowość")]
        public int Ingenuity { get; set; }

        [Display(Name = "Średnia ocena")]
        public float AverageGrade { get; set; }

        [ForeignKey("Rater")]
        public string RaterId { get; set; }
        public User Rater { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}