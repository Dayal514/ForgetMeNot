namespace ForgetMeNot.Models.DataModels
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime? DueDateUtc { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedUtc { get; set; }

        // relationship
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
