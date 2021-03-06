﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.WebPages;
using IdeaCreationManagement.Models;

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
        [DisplayName("Status emaila")]
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        [DisplayName("Kategoria")]
        public string Category { get; set; }

        [DisplayName("Role")]
        public string RoleNames
        {
            get
            {
                string output = "";
                foreach (var x in Roles)
                {
                    if (!output.IsEmpty())
                    {
                        output += ", ";
                    }
                    output += x;
                }
                return output;
            }
        }
    }

    public class UserDetails : ListUser
    {
        // student data
        [DisplayName("Numer albumu")]
        public int StudentNumber { get; set; }
        public int? FieldOfStudyId { get; set; }

        [DisplayName("Kierunek studiów")]
        public string FieldOfStudy { get; set; }

        // employee data
        public int? OrganizationalUnitId { get; set; }

        [DisplayName("Jednostka organizacyjna")]
        public string OrganizationalUnit { get; set; }

        public int? CategoryId { get; set; }

        [DisplayName("Kategoria")]
        public string Category { get; set; }
    }

    public class UserEditViewModel : ListUser
    {
        public int StudentNumber { get; set; }

        [DisplayName("Numer albumu")]
        public string StudentNumberView => StudentNumber == 0 ? "" : StudentNumber.ToString();

        public int? FieldOfStudyId { get; set; }
        [DisplayName("Kierunek studiów")]
        public List<SelectListItem> FieldsOfStudy { get; set; }

        public int? OrganizationalUnitId { get; set; }
        [DisplayName("Jednostka organizacyjna")]
        public List<SelectListItem> OrganizationalUnits { get; set; }

        public int? CategoryId { get; set; }
        [DisplayName("Kategoria")]
        public List<SelectListItem> Categories { get; set; }

        public bool IsEmployee { get; set; }
        public bool IsStudent { get; set; }
        public List<ListProject> AssignedProjects { get; set; }
    }

    public class UserEditSubmitModel
    {
        [MaxLength(100, ErrorMessage = "Zbyt długie imię")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Zbyt długie nazwisko")]
        public string Surname { get; set; }
        [RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int? StudentNumberView { get; set; }
        public int? FieldOfStudyId { get; set; }
        public int? OrganizationalUnitId { get; set; }
        public int? CategoryId { get; set; }

        public int StudentNumber => StudentNumberView ?? 0;
    }

    public class ListProject
    {
        public int Id { get; set; }

        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [DisplayName("Kategoria")]
        public string Category { get; set; }

        [DisplayName("Stan")]
        public string State { get; set; }

        public string AuthorId { get; set; }

        [DisplayName("Autor")]
        public string Author { get; set; }
        public string AssigneeId { get; set; }

        [DisplayName("Przydzielony")]
        public string Assignee { get; set; }
    }

    public class UserDetailsViewModel
    {
        public UserDetails Details { get; set; }
        public ICollection<ListProject> CreatedProjects { get; set; }
        public ICollection<ListProject> AssignedProjects { get; set; }
    }

    public class DeassignViewModel
    {
        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        public Project Project { get; set; }
    }
}