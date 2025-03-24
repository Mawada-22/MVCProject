using Demo.BLL.Dtos;
using Demo.BLL.Services.DepartmentServicea;
using Demo.BLL.Services.EmployeeServices;
using Demo.pl.Models.Departmets;
using Demo.pl.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Demo.pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _services = employeeServices;
        }
        [HttpGet] //Get: /Departments?Index
        public IActionResult Index()
        {

            var Emps = _services.GetAllEmpolyees();
            return View(Emps);

        }
        [HttpGet] 
        public IActionResult Create()
        {
            return View();


        }

        [HttpPost]
        public IActionResult Create(CreateEmpDto  createEmpDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createEmpDto);
            }

            var res = _services.CreateEmployee(createEmpDto);

            if (res > 0) { return RedirectToAction(nameof(Index)); }
            else
            {
                ModelState.AddModelError(string.Empty, "Employee is not created");
                return View(createEmpDto);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var emp = _services.GetEmployeeById(id.Value);


            if (emp == null) { return NotFound(); };
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var emp = _services.GetEmployeeById(id.Value);


            if (emp == null) { return NotFound(); };

            return View(new EmployeeEditModelView()
            {
                Name = emp.Name,
                Salary = emp.Salary,
                phonenumber = emp.PhoneNumber,
                Address = emp.Address,
                Email = emp.Email,
                Age = emp.Age,
                IsActive = emp.IsActive,
                HiringDate = emp.HiringDate,
                EmpType = emp.EmpType,
                gender = emp.gender
            });


        }


        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeEditModelView employeeEditModelView)
        {
            if (!ModelState.IsValid) return View(employeeEditModelView);

            var Emp = new UpdateEmpDto() {Name = employeeEditModelView.Name,
            Salary= employeeEditModelView.Salary,
            Age= employeeEditModelView.Age,
            Email = employeeEditModelView.Email,
            EmpType = employeeEditModelView.EmpType,
            phonenumber = employeeEditModelView.phonenumber
            };
            var x = _services.UpdateEmployee(Emp);
            if (x > 0) { return RedirectToAction(nameof(Index)); }
            else { return BadRequest(); }


        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var emp = _services.GetEmployeeById(id.Value);

            if (emp == null) { return NotFound(); }
            return View(emp);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            var msg = string.Empty;
            try
            {
                var deleted = _services.DeltedEmployee(id);
                if (deleted) return RedirectToAction(nameof(Index));

                msg = "an error Ocurred During deleting the Depaertment:(";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                msg = "An error occurred during deletion.";

            }

            return RedirectToAction(nameof(Index));
        }
    }
}
