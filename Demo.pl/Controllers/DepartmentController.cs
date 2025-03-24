using Demo.BLL.Dtos;
using Demo.BLL.Services.DepartmentServicea;
using Demo.pl.Models.Departmets;
using Microsoft.AspNetCore.Mvc;

namespace Demo.pl.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _services;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _services = departmentServices;
        }
        [HttpGet] //Get: /Departments?Index
        public IActionResult Index()
        {
            //returns views with the needed model"all departments"

            var departments = _services.GetAllDepartments();
            return View(departments);

        }
        [HttpGet] //get:department/create
        public IActionResult Create()
        {
            return View();


        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }

            var res = _services.CreateDepartment(departmentDto);

            if (res > 0) { return RedirectToAction(nameof(Index)); }
            else
            {
                ModelState.AddModelError(string.Empty, "Department is not created");
                return View(departmentDto);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _services.GetDepartmentById(id.Value);


            if (department == null) { return NotFound(); };
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)return BadRequest();
            var department = _services.GetDepartmentById(id.Value);


            if (department == null) { return NotFound(); };

            return View(new CreateDepartmentDto() {Code= department.Code,Name=department.Name,Description=department.Description,CreationDate=department.CreationDate });

                                                                   
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmetEditViewModel departmetEditViewModel)
        {
            if (!ModelState.IsValid) return View(departmetEditViewModel);

            var department = new UpdateDepartmentDto() { Id=id,Code = departmetEditViewModel.Code, Name = departmetEditViewModel.Name, Description = departmetEditViewModel.Description, CreationDate = departmetEditViewModel.CreationDate };
           
            var x =_services.UpdateDepartment(department);
            if (x > 0) { return RedirectToAction(nameof(Index)); }
            else { return BadRequest(); }


        }
    }
}
