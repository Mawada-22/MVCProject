using Demo.BLL.Dtos;
using Demo.BLL.Services.DepartmentServicea;
using Demo.pl.Models.Departmets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Demo.pl.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _services;
        private readonly ILogger<DepartmentController>_logger;
        private readonly IWebHostEnvironment _environment;
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
        [IgnoreAntiforgeryToken]
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
            if(id is null )return BadRequest(); //400
            var department = _services.GetDepartmentById(id.Value);


            if (department == null) { return NotFound(); };

            return View(new DepartmetEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate

            });

                                                                   
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit([FromRoute]int id,DepartmetEditViewModel departmetEditViewModel)
        {
            if (!ModelState.IsValid) return View(departmetEditViewModel);
            var msg = string.Empty;
            try
            {
                var department = new UpdateDepartmentDto() { Id = id, Code = departmetEditViewModel.Code, Name = departmetEditViewModel.Name, Description = departmetEditViewModel.Description, CreationDate = departmetEditViewModel.CreationDate };

                var x = _services.UpdateDepartment(department);
                if (x > 0) { return RedirectToAction(nameof(Index)); }

                msg = "an Error Ocurred while editing department";

            }
            catch (Exception ex)
            {
                //1-log
                _logger.LogError(ex, msg);
                //2.set massage 
                msg = _environment.IsDevelopment() ? ex.Message : "An Error ocurred while updating";
                
            }
            return View(departmetEditViewModel);    

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(!id.HasValue) return BadRequest();

            var department = _services.GetDepartmentById(id.Value);

            if (department == null) { return NotFound(); }
            return View(department);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete([FromRoute]int id)
        {
            var msg = string.Empty;
            try
            {
               var deleted = _services.DeltedDepartment(id);
                if (deleted) return RedirectToAction(nameof(Index));

                msg = "an error Ocurred During deleting the Depaertment:(";

            }
            catch (Exception ex)
            {
                //1-log the exception
                _logger.LogError(ex, ex.Message);

                //2- set msg
                msg = _environment.IsDevelopment()     ? ex.Message : "\"an error Ocurred During deleting the Depaertment:(";
            }

           return RedirectToAction(nameof(Index));
        }
    }
}
