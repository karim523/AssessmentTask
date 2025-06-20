using AutoMapper;
using Demo.BLL.Dtos.Employees;
using Demo.BLL.Services.Employees;
using Demo.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeServices _employeeServices;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ILogger<DepartmentController> logger,
            IWebHostEnvironment env, IEmployeeServices employeeServices, IMapper mapper)
        {
            _logger = logger;
            _env = env;
            _employeeServices = employeeServices;
            _mapper = mapper;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var departments = await _employeeServices.GetAllEmployeesAsync(SearchValue);
            return View(departments);
        }
        #endregion

        #region Search
        [HttpGet]
        public async Task<IActionResult> Search(string SearchValue)
        {
            var employees = await _employeeServices.GetAllEmployeesAsync(SearchValue);
            return PartialView("Partials/EmployeesTable", employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentService _departmentServices*/)
        {
            //ViewData["Departments"] = _departmentServices.GetAllDepartments();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModels employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var message = string.Empty;
            try
            {
                var employeeToCreated = _mapper.Map<EmployeeViewModels, CreatedEmployeeDto>(employeeDto);

                var restult = await _employeeServices.CreateEmployeeAsync(employeeToCreated);
                if (restult > 0)
                {
                    TempData["Message"] = "Employee created successfully";
                }
                else
                {
                    message = "Employee creation failed";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeDto);
                }
                else
                {
                    message = "An error occurred while creating the department";
                    return View("Error", message);
                }
            }
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var department = await _employeeServices.GetEmployeeByIdAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee = await _employeeServices.GetEmployeeByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeViewModel = _mapper.Map<EmployeeDetailsDto, EmployeeViewModels>(employee);
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModels employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var message = string.Empty;
            try
            {
                var emploeeToUpdated = _mapper.Map<UpdatedEmployeeDto>(employeeDto);
                var result = await _employeeServices.UpdateEmployeeAsync(emploeeToUpdated);

                if (result >= 0)
                    TempData["Message"] = "Employee updated successfully";
                else
                {
                    message = "Employee update failed";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                }
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Employee can not update ";
            }
            return View(employeeDto);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var department = await _employeeServices.DeleteEmployeeAsync(id);
            var message = string.Empty;
            try
            {
                if (department)
                    TempData["Message"] = "Employee deleted successfully";

                else
                {
                    message = "An error happened when deleting the Employee";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An error happened when deleting the Employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion
    }
}
