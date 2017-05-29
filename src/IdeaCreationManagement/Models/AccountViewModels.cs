using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

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
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterStudentViewModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression("^([\\w\\.\\-]+)@stud.prz.edu.pl", ErrorMessage = "Wprowadź poprawny adres email, zawierający @stud.prz.edu.pl")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2 } znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("FieldOfStudy")]
        public int Id { get; set; }
        [Display(Name = "Kierunek studiów")]
        public virtual FieldOfStudy Name { get; set; }
    }

    public class RegisterEmployeeViewModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression("^([\\w\\.\\-]+)@prz.edu.pl", ErrorMessage = "Wprowadź poprawny adres email, zawierający @prz.edu.pl")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("OrganizationalUnit")]
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
