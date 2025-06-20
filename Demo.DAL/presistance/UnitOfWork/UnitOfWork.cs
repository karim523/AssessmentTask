using Demo.DAL.presistance.Data;
using Demo.DAL.presistance.Repostories.Departments;
using Demo.DAL.presistance.Repostories.Employees;
using Demo.DAL.presistance.Repostories.Tasks;

namespace Demo.DAL.presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IEmployeeRepository EmployeeRepository =>  new EmployeeRepository(_context);

        public IDepartmentRepository DepartmentRepository =>  new DepartmentRepository(_context);

        public IEmployeeTaskRepository EmployeeTaskRepository =>  new EmployeeTaskRepository(_context);

        public async Task<int> CommitAsync()=>await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}
