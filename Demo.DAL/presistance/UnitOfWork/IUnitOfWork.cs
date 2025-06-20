using Demo.DAL.presistance.Repostories.Departments;
using Demo.DAL.presistance.Repostories.Employees;
using Demo.DAL.presistance.Repostories.Tasks;

namespace Demo.DAL.presistance.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeTaskRepository EmployeeTaskRepository { get; }
        Task<int> CommitAsync();
    }
}
