using System.Collections.Generic;

namespace IdeaCreationManagement.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}