using Demo.BLL.Dtos.Departments;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
       Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();
       Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int id);
       Task<int> CreateDepartmentAsync(DepartmentToCreateDto departmentToCreateDto);
       Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto departmentToUpdateDto);
       Task<bool> DeleteDepartmentAsync(int id);

        Task<DepartmentSearchResult?> Search(string SearchValue);
    }
}
