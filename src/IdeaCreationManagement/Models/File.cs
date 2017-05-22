namespace IdeaCreationManagement.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public byte[] Content { get; set; }
        public long Size { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}