using Demo.DAL.Entities.Tasks;
using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repostories._Generic;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.presistance.Repostories.Tasks
{
    public class EmployeeTaskRepository : GenericRepostory<EmployeeTask>, IEmployeeTaskRepository
    {
        public EmployeeTaskRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<EmployeeTask>> GetTasksByManagerAsync(int managerId)
        {
            return await _context.EmployeeTasks
                .Where(t => t.ManagerId == managerId)
                .Include(t => t.Employee)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeTask>> GetTasksForEmployeeAsync(int employeeId)
        {
            return await _context.EmployeeTasks
                .Where(t => t.EmployeeId == employeeId)
                .ToListAsync();
        }
    }

}
