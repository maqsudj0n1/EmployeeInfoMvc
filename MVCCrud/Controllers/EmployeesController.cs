using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCrud.Data;
using MVCCrud.Models;
using MVCCrud.Models.Domain;

namespace MVCCrud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudDbContext _dbContext;

        public EmployeesController(CrudDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary,
                DateOfBirth = model.DateOfBirth,
                Department = model.Department
            };
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth=employee.DateOfBirth,
                    Department = employee.Department
                };
                return await Task.Run(() => View("View" , viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _dbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await _dbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
