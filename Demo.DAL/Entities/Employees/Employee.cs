using Demo.DAL.Entities.Departments;
using System.ComponentModel.DataAnnotations.Schema;
namespace Demo.DAL.Entities.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public decimal Salary{ get; set; }
        public string? Image { get; set; }
        public int? ManagerId { get; set; }
        public virtual Employee? Manager { get; set; }
        [NotMapped]
        public string ManagerName => Manager != null ? $"{Manager.FirstName} {Manager.LastName}" : "No Manager"; public string? Email { get; set; }
        public virtual  Department? Department { get; set; } 
        public int? DepartmentId { get; set; }

    }
}
