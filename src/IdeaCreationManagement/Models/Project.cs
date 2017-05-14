namespace IdeaCreationManagement.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int? AssigneeId { get; set; }
        public User Assignee { get; set; }
        public ProjectType Type { get; set; }
        public float AverageGrade { get; set; }
        public float AverageUsefulness { get; set; }
        public float AverageDifficulty { get; set; }
        public float AverageIngenuity { get; set; }
        public int StateIs { get; set; }
        public State State { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}