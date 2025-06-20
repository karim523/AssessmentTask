using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Dtos.Employees;
namespace Demo.BLL.Services.Employees
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string SearchValue);
        Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);
        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);

    }
}
