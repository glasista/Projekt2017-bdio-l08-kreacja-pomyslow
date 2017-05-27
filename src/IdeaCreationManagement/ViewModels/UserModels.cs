using System.Collections.Generic;
using System.ComponentModel;

namespace IdeaCreationManagement.ViewModels
{
    public class ListUser
    {
        public string Id { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Zatwierdzony")]
        public bool EmailConfirmed { get; set; }
        [DisplayName("Role")]
        public List<string> RoleNames { get; set; }
    }
}