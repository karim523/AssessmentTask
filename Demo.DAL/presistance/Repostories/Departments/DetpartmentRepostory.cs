using Demo.DAL.Entities.Departments;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repostories._Generic;

namespace Demo.DAL.presistance.Repostories.Departments
{
    public class DepartmentRepository : GenericRepostory<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }

     
    }
}
