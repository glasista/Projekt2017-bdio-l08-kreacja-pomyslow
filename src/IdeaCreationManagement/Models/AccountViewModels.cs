using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterStudentViewModel
    {
        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić nazwisko")]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić email")]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression("^([\\w\\.\\-]+)@stud.prz.edu.pl", ErrorMessage = "Wprowadź poprawny adres email, zawierający @stud.prz.edu.pl")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2 } znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("FieldOfStudy")]
        [Required(ErrorMessage = "Musisz wybrać kierunek studiów z listy")]
        public int Id { get; set; }
        [Display(Name = "Kierunek studiów")]
        public virtual FieldOfStudy Name { get; set; }
    }

    public class RegisterEmployeeViewModel
    {
        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić naziwsko")]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić email")]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression("^([\\w\\.\\-]+)@prz.edu.pl", ErrorMessage = "Wprowadź poprawny adres email, zawierający @prz.edu.pl")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("OrganizationalUnit")]
        [Required(ErrorMessage = "Musisz wybrać zakład/katedrę z listy")]
        public int Id { get; set; }
        [Display(Name = "Zakład/Katedra")]
        public virtual OrganizationalUnit Name { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
