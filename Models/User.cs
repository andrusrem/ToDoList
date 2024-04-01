namespace ToDoList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string NewPassword { get; set; }
        public string Access_token { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}