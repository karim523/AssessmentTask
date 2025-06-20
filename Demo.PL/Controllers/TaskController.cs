using Demo.BLL.Dtos.Tasks;
using Demo.DAL.Entities.Tasks;
using Demo.DAL.presistance.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            var result = tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                DueDate = t.DueDate,
                Status = t.Status.ToString(),
                EmployeeName = $"{t.Employee?.FirstName} {t.Employee?.LastName}"
            });

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            int managerId = GetCurrentManagerId();

            var employees = await _unitOfWork.EmployeeRepository
                .GetAllQueryable()
                .Where(e => e.ManagerId == managerId)
                .Select(e => new
                {
                    e.Id,
                    FullName = $"{e.FirstName} {e.LastName}"
                })
                .ToListAsync();

            ViewBag.Employees = new SelectList(employees, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                return await Create(); 
            }

            var task = new EmployeeTask
            {
                Title = dto.Title,
                DueDate = dto.DueDate,
                Status = DAL.Common.Enums.TaskStatus.Pending,
                EmployeeId = dto.EmployeeId,
                ManagerId = GetCurrentManagerId()
            };

            _unitOfWork.EmployeeTaskRepository.Add(task);
            await _unitOfWork.CommitAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> MyTasks()
        {
            int employeeId = GetCurrentEmployeeId();
            var tasks = await _unitOfWork.EmployeeTaskRepository.GetTasksForEmployeeAsync(employeeId);

            var result = tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                DueDate = t.DueDate,
                Status = t.Status.ToString()
            });

            return View(result);
        }

        public async Task<IActionResult> EditStatus(int id)
        {
            var task = await _unitOfWork.EmployeeTaskRepository.GetByIdAsync(id);
            if (task == null || task.EmployeeId != GetCurrentEmployeeId())
                return NotFound();

            var dto = new UpdateTaskStatusDto
            {
                Id = task.Id,
                Status = (TaskStatus)task.Status
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(UpdateTaskStatusDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var task = await _unitOfWork.EmployeeTaskRepository.GetByIdAsync(dto.Id);
            if (task == null || task.EmployeeId != GetCurrentEmployeeId())
                return NotFound();

            task.Status = (DAL.Common.Enums.TaskStatus)dto.Status;
            _unitOfWork.EmployeeTaskRepository.Update(task);
            await _unitOfWork.CommitAsync();

            return RedirectToAction(nameof(MyTasks));
        }


        private int GetCurrentManagerId()
        {
            return 2;
        }

        private int GetCurrentEmployeeId()
        {
            return 4;
        }
    }
}
