using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title can't be more than 100 characters.")]
        public string Title { get; set; } = null!;

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Assign To Employee")]
        [Required(ErrorMessage = "Please select an employee.")]
        public int EmployeeId { get; set; }
    }
  
}
