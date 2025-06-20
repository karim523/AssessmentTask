using Demo.DAL.Entities.Employees;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repostories._Generic;

namespace Demo.DAL.presistance.Repostories.Employees
{
    public class EmployeeRepository : GenericRepostory<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }
    }
}
