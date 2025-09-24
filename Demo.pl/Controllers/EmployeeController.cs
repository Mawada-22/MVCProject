using Demo.BLL.Dtos;
using Demo.BLL.Services.DepartmentServicea;
using Demo.BLL.Services.EmployeeServices;
using Demo.pl.Models.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Demo.pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentServices _departmentService;
        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment, IDepartmentServices departmentServices)
        {
            _services = employeeServices;
            _environment = environment;
            _logger = logger;
            _departmentService = departmentServices;
        }
        [HttpGet] //Get: /Departments?Index
        public IActionResult Index(string search)
        {

            var Emps = _services.GetEmpolyees(search);
            return View(Emps);

        }
        [HttpGet] 
        public IActionResult Create()
        {
            ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();


        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(EmployeeModelView  employeeModelView)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeModelView);
            }

            var res = _services.CreateEmployee(new CreateEmpDto() {Name=employeeModelView.Name,Email=employeeModelView.Email,Address=employeeModelView.Address,IsActive=employeeModelView.IsActive,EmpType=employeeModelView.EmpType,Age=employeeModelView.Age,Salary=employeeModelView.Salary,PhoneNumber=employeeModelView.phonenumber,gender=employeeModelView.gender, Departmentid=employeeModelView.DepartmentID});

            if (res > 0) { return RedirectToAction(nameof(Index)); }
            else
            {
                ModelState.AddModelError(string.Empty, "Employee is not created");
                return View(employeeModelView);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            ViewData["Department"] = _departmentService.GetAllDepartments();

            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Emp = _services.GetEmployeeById(id.Value);


            if (Emp == null) { return NotFound(); };
            return View(Emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           
            if (!id.HasValue) return BadRequest();
            var emp = _services.GetEmployeeById(id.Value);


            if (emp == null) { return NotFound(); };

            ViewData["Departments"] = _departmentService.GetAllDepartments();


            return View(new EmployeeModelView()
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
                gender = emp.gender,
                DepartmentID=emp.Departmentid,
                DepartmentName=emp.DepartmentName
            });


        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeModelView employeeEditModelView)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Departments"] = _departmentService.GetAllDepartments();
                return View(employeeEditModelView);
            }

            var Emp = new UpdateEmpDto()
            {
                Id = id,
                Name = employeeEditModelView.Name,
                Salary = employeeEditModelView.Salary,
                Age = employeeEditModelView.Age,
                Email = employeeEditModelView.Email,
                EmpType = employeeEditModelView.EmpType,
                phonenumber = employeeEditModelView.phonenumber,
                gender=employeeEditModelView.gender,



                DepartmentId = employeeEditModelView.DepartmentID.HasValue && employeeEditModelView.DepartmentID > 0
                                ? employeeEditModelView.DepartmentID
                                : null
            };

            var x = _services.UpdateEmployee(Emp);

            if (x > 0)
                return RedirectToAction(nameof(Index));

            return BadRequest();
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
        [IgnoreAntiforgeryToken]
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
