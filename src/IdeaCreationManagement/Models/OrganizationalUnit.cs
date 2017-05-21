using System.ComponentModel.DataAnnotations;

namespace IdeaCreationManagement.Models
{
    public class OrganizationalUnit
    {
        public int Id { get; set; }

        [Display(Name = "Jednostka uczelni")]
        public string Name { get; set; }
    }
}