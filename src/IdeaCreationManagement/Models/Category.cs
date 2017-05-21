using System.ComponentModel.DataAnnotations;

namespace IdeaCreationManagement.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Kategoria")]
        public string Name { get; set; }
        public ProjectType Type { get; set; }
    }
}