using Demo.DAL.Entities.Employees;

namespace Demo.DAL.Entities.Departments
{
    public class Department 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
