﻿using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Zatwierdzony")]
        public bool EmailConfirmed { get; set; }
        [DisplayName("Role")]
        public List<string> RoleNames { get; set; }
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