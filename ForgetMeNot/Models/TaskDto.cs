namespace ForgetMeNot.Models
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime? DueDateUtc { get; set; }
    }

    public class UpdateTaskDto
    {
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime? DueDateUtc { get; set; }
        public bool IsDone { get; set; }
    }

    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime? DueDateUtc { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? CompletedUtc { get; set; }
    }
}
