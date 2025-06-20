using Demo.DAL.Entities.Employees;
using System.ComponentModel.DataAnnotations;
using TaskStatus = Demo.DAL.Common.Enums.TaskStatus;

namespace Demo.DAL.Entities.Tasks
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public int ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }
    }
}
