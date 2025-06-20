namespace Demo.BLL.Dtos.Tasks
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = null!;
        public string? EmployeeName { get; set; }
    }
}
