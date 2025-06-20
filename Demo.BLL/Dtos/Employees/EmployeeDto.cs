using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Dtos.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } 

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public string? Department { get; set; }

        public string? Image { get; set; }

        public string? ManagerName { get; set; }

    }
}
