using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Employees
{
    public class EmployeeViewModels
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Max Length of First Name is 50 characters")]
        public string FirstName { get; set; } = null!;
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Max Length of Last Name is 50 characters")]
        public string LastName { get; set; } = null!;
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
        public IFormFile? Image { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public string? DepartmentName { get; set; }
        public string? ManagerName { get; set; }
        public string? ImageUrl { get; set; }

    }
}
