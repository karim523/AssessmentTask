using AutoMapper;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Dtos.Employees;
using Demo.PL.ViewModels.Departments;
using Demo.PL.ViewModels.Employees;

namespace Demo.PL.Mapping.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region DepartmentModule
            CreateMap<DepartmentViewModels, DepartmentToCreateDto>();
            //.ForMember(dest=>dest.Name,config=>config.MapFrom(src=>src.DepartmentName));

            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModels>();

            CreateMap<DepartmentViewModels, DepartmentToUpdateDto>();

            #endregion

            #region EmployeeDepartment

            CreateMap<EmployeeViewModels, CreatedEmployeeDto>();

            CreateMap<EmployeeDetailsDto, EmployeeViewModels>();

            CreateMap<EmployeeViewModels, UpdatedEmployeeDto>(); 
            #endregion
        }
    }
}
