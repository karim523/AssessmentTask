using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Departments
{
    public class DepartmentToCreateDto
    {
        [Required(ErrorMessage ="Name is required , please enter the name ")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
    }
}
