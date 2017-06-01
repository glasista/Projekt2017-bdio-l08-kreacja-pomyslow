using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaCreationManagement.Models
{
    public class File
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        public string DataType { get; set; }
        public byte[] Content { get; set; }

        [Display(Name = "Rozmiar")]
        public long Size { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}