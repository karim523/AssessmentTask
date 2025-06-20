using Demo.BLL.Common.Services.AttachmentService;
using Demo.BLL.Dtos.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeServices(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId,
                ManagerId = employeeDto.ManagerId
            };

            if (employeeDto.Image is not null)
            {
                employee.Image = await _attachmentService.UploadAsync(employeeDto.Image, "images");
            }

            _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeDto.Id);
            if (employee is null)
                return 0;

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Salary = employeeDto.Salary;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.ManagerId = employeeDto.ManagerId;

            if (employeeDto.Image is not null)
            {
                employee.Image = await _attachmentService.UploadAsync(employeeDto.Image, "images");
            }

            _unitOfWork.EmployeeRepository.Update(employee);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is null)
                return false;

            _unitOfWork.EmployeeRepository.Delete(employee);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string searchValue)
        {
            var query = _unitOfWork.EmployeeRepository.GetAllQueryable()
                .Include(emp => emp.Department)
                .Include(emp => emp.Manager)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.ToLower();
                query = query.Where(x =>
                    (x.FirstName + " " + x.LastName).ToLower().Contains(searchValue)
                    || x.FirstName.ToLower().Contains(searchValue)
                    || x.LastName.ToLower().Contains(searchValue));
            }

            return await query.Select(emp => new EmployeeDto
            {
                Id = emp.Id,
                FullName = emp.FirstName + " " + emp.LastName,
                Salary = emp.Salary,
                Department = emp.Department != null ? emp.Department.Name : "No Department",
                Image = emp.Image,
                ManagerName = emp.Manager != null ? emp.Manager.FirstName + " " + emp.Manager.LastName : null
            }).ToListAsync();
        }

        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetAllQueryable()
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee is null)
                return null;

            return new EmployeeDetailsDto
            {
                Id = employee.Id,
                FullName = employee.FirstName + " " + employee.LastName,
                Salary = employee.Salary,
                Department = employee.Department?.Name ?? "No Department",
                Image = employee.Image,
                ManagerName = employee.Manager != null
                    ? employee.Manager.FirstName + " " + employee.Manager.LastName
                    : null
            };
        }
    }
}