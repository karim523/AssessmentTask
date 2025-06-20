using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Tasks
{
    public class UpdateTaskStatusDto
    {
        public int Id { get; set; }

        [Required]
        public TaskStatus Status { get; set; }
    }
}
