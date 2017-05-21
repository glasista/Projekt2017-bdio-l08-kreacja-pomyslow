using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdeaCreationManagement.Models
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Stan")]
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}