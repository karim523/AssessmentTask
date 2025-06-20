using Demo.BLL.Dtos.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {         
            var departments= await _unitOfWork.DepartmentRepository.GetAllQueryable().Select(d => new DepartmentToReturnDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Code = d.Code
            }).AsNoTracking().ToListAsync();
            return departments;
        }

        public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department is not null)
            {
                return new DepartmentDetailsToReturnDto()
                {
                    Id = department.Id,               
                    Name = department.Name,
                    Description = department.Description,
                    Code = department.Code
                };
            }
            return null!;
        }
        public async Task<int> CreateDepartmentAsync(DepartmentToCreateDto departmentToCreateDto)
        {
            var departmentCreated = new Department()
            {
                Code = departmentToCreateDto.Code,
                Name = departmentToCreateDto.Name,
                Description = departmentToCreateDto.Description
            };
             _unitOfWork.DepartmentRepository.Add(departmentCreated);
            return await _unitOfWork.CommitAsync();
        }
        public async Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto departmentToUpdateDto)
        {
            var departmentCreated = new Department()
            {
                Id = departmentToUpdateDto.Id,
                Code = departmentToUpdateDto.Code,
                Name = departmentToUpdateDto.Name,
                Description = departmentToUpdateDto.Description    
            };

             _unitOfWork.DepartmentRepository.Update(departmentCreated);
            return await _unitOfWork.CommitAsync();

        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            
            var department = await departmentRepo.GetByIdAsync(id);
            
            if (department is not null)
                departmentRepo.Delete(department);

            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<DepartmentSearchResult?> Search(string SearchValue)
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllQueryable()
        .Where(d => d.Name.ToLower().Contains(SearchValue.ToLower().Trim()));

            if (!departments.Any())
                return null;

            var employeeCount = departments.SelectMany(d => d.Employees).Count();
            var totalSalary = departments.SelectMany(d => d.Employees).Sum(e => e.Salary);

            return new DepartmentSearchResult
            {
                DepartmentName = SearchValue,
                EmployeeCount = employeeCount,
                TotalSalary = totalSalary
            };
        }
    }
}
