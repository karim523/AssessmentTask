using AutoMapper;
using Demo.BLL.Dtos.Departments;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Demo.PL.Controllers
{
  
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger,
            IWebHostEnvironment env,IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _env = env;
            _mapper = mapper;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }
        #endregion

        #region Search
        [HttpGet]
        public async Task<IActionResult> Search(string SearchValue)
        {
            var departments = await _departmentService.Search(SearchValue);
            return PartialView("Partials/SearchResult", departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModels departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var message = string.Empty;
            try
            {
                var departmentToCreated = _mapper.Map<DepartmentViewModels, DepartmentToCreateDto>(departmentDto);
                var restult =await _departmentService.CreateDepartmentAsync(departmentToCreated);
                if (restult > 0)
                {
                    TempData["Message"] = "Department created successfully";
                }
                else
                {
                    message = "Department creation failed";
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
                    return View(departmentDto);
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
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
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
                return BadRequest();
            
            var department =await _departmentService.GetDepartmentByIdAsync(id.Value);
          
            if (department == null)
                return NotFound();
            
            var departmentViewModels = _mapper.Map<DepartmentDetailsToReturnDto, DepartmentViewModels>(department);
            return View(departmentViewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModels departmentViewModels)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModels);
            }
            var message = string.Empty;
            try
            {
                var departmentToUpdated = _mapper.Map<DepartmentToUpdateDto>(departmentViewModels);
                departmentToUpdated.Id = id;
                var result =await _departmentService.UpdateDepartmentAsync(departmentToUpdated);
               
                if (result >= 0)
                    TempData["Message"] = "Department updated successfully";
                
                else
                {
                    message = "Department update failed";
                    TempData["Message"] = message;
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Department can not update ";
            }
            return View(departmentViewModels);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var department =await _departmentService.DeleteDepartmentAsync(id);
            var message = string.Empty;
            try
            {
                if (department)
                {
                    TempData["Message"] = "Department deleted successfully";

                }
                else
                {
                    message = "An error happened when deleting the department";
                    TempData["Message"] = message;

                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An error happened when deleting the department";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        } 
        #endregion
    }
}