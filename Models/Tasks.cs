namespace ToDoList.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Desc { get; set; }
        public bool Marked_as_done { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}