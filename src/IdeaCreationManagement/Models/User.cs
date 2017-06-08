using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace IdeaCreationManagement.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public int? FieldOfStudyId { get; set; }
        public FieldOfStudy FieldOfStudy { get; set; }
        public int? OrganizationalUnitId { get; set; }
        [Display(Name = "Katedra")]
        public OrganizationalUnit OrganizationalUnit { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        public string PasswordHashed { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        public int StudentNumber { get; set; }

        [InverseProperty("Author")]
        public IList<Project> CreateProjects { get; set; }
        [InverseProperty("Assignee")]
        public IList<Project> AssignedProjects { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}