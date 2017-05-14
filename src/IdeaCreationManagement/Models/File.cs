namespace IdeaCreationManagement.Models
{
    public class File
    {
        public int FileId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long Size { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}