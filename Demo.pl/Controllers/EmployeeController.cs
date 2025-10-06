using AutoMapper;
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
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentServices _departmentService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment, IDepartmentServices departmentServices, IMapper mapper)
        {
            _services = employeeServices;
            _environment = environment;
            _logger = logger;
            _departmentService = departmentServices;
            _mapper = mapper;
        }
        [HttpGet] //Get: /Departments?Index
        public async Task<IActionResult> Index(string search)
        {

            var Emps =await _services.GetEmpolyeesAsync(search);
            return View(Emps);

        }
        [HttpGet] 
        public async Task<IActionResult> Create()
        {
            ViewData["Departments"] = await _departmentService.GetAllDepartmentsAsync();
            return View();


        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public  async Task<IActionResult> Create(EmployeeModelView  employeeModelView)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeModelView);
            }
            var tobecreated = _mapper.Map<EmployeeModelView, CreateEmpDto>(employeeModelView);
            var res = await _services.CreateEmployeeAsync(tobecreated);
           // var res = _services.CreateEmployee(new CreateEmpDto() {Name=employeeModelView.Name,Email=employeeModelView.Email,Address=employeeModelView.Address,IsActive=employeeModelView.IsActive,EmpType=employeeModelView.EmpType,Age=employeeModelView.Age,Salary=employeeModelView.Salary,PhoneNumber=employeeModelView.phonenumber,gender=employeeModelView.gender, Departmentid=employeeModelView.DepartmentID});

            if (res > 0) { return RedirectToAction(nameof(Index)); }
            else
            {
                ModelState.AddModelError(string.Empty, "Employee is not created");
                return View(employeeModelView);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Department"] = await _departmentService.GetAllDepartmentsAsync();

            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Emp = await _services.GetEmployeeByIdAsync(id.Value);


            if (Emp == null) { return NotFound(); };
            return View(Emp);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (!id.HasValue) return BadRequest();
            var emp =await _services.GetEmployeeByIdAsync(id.Value);


            if (emp == null) { return NotFound(); };

            ViewData["Departments"] = await _departmentService.GetAllDepartmentsAsync();
            var EmpVM = _mapper.Map<EmpDetailsDto, EmployeeModelView>(emp);
           /* return View(new EmployeeModelView()
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
            });*/

            return View(EmpVM);


        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeModelView employeeEditModelView)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Departments"] =await _departmentService.GetAllDepartmentsAsync();
                return View(employeeEditModelView);
            }


            /*
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
            };*/


            var empDto = _mapper.Map<UpdateEmpDto>(employeeEditModelView);
            empDto.Id = id; // ensure the correct ID is set

            var result =await _services.UpdateEmployeeAsync(empDto);


            if (result > 0)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var emp = await _services.GetEmployeeByIdAsync(id.Value);

            if (emp == null) { return NotFound(); }
            return View(emp);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var msg = string.Empty;
            try
            {
                var deleted = await _services.DeltedEmployeeAsync(id);
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
