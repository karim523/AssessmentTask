using Demo.DAL.Entities.Tasks;
using Demo.DAL.presistance.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PL.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            int managerId = GetCurrentManagerId();
            var tasks = await _unitOfWork.EmployeeTaskRepository.GetTasksByManagerAsync(managerId);
            return View(tasks);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = new SelectList(
                await _unitOfWork.EmployeeRepository.GetAllAsync(),
                "Id", "FullName"
            );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeTask model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = new SelectList(
                    await _unitOfWork.EmployeeRepository.GetAllAsync(),
                    "Id", "FullName"
                );
                return View(model);
            }

            model.ManagerId = GetCurrentManagerId();
            model.AssignedDate = DateTime.Now;
            _unitOfWork.EmployeeTaskRepository.Add(model);
            await _unitOfWork.CommitAsync();

            return RedirectToAction("Index");
        }

        private int GetCurrentManagerId()
        {
            return 2;
        }
    }
}
