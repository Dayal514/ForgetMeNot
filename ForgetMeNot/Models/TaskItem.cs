namespace ForgetMeNot.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDone { get; set; }

        // relationship
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
