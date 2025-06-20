using Demo.DAL.Entities.Tasks;
using Demo.DAL.presistance.Repostories._Generic;

namespace Demo.DAL.presistance.Repostories.Tasks
{
    public interface IEmployeeTaskRepository : IGenericRepostory<EmployeeTask>
    {
        Task<IEnumerable<EmployeeTask>> GetTasksByManagerAsync(int managerId);
        Task<IEnumerable<EmployeeTask>> GetTasksForEmployeeAsync(int employeeId);
    }
}
