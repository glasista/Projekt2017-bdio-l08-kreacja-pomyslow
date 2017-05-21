using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Data dodania")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        [Display(Name = "Autor")]
        public User Author { get; set; }

        [ForeignKey("Assignee")]
        public string AssigneeId { get; set; }

        public User Assignee { get; set; }
        public ProjectType Type { get; set; }

        [Display(Name = "Średnia ocena")]
        public float AverageGrade { get; set; }
        public float AverageUsefulness { get; set; }
        public float AverageDifficulty { get; set; }
        public float AverageIngenuity { get; set; }

        public int StateId { get; set; }

        [Display(Name = "Stan")]
        public State State { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public Category Category { get; set; }
    }
}