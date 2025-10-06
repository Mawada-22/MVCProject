using AutoMapper;
using Demo.BLL.Dtos;
using Demo.BLL.Services.DepartmentServicea;
using Demo.pl.Models.Departmets;
using Microsoft.AspNetCore.Mvc;

namespace Demo.pl.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _services;
        private readonly ILogger<DepartmentController>_logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
    
        public DepartmentController(IDepartmentServices departmentServices, ILogger<DepartmentController> logger, IWebHostEnvironment environment,IMapper mapper)
        {
            _services = departmentServices;
            _environment = environment;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet] //Get: /Departments?Index
        public async Task<IActionResult> Index()
        {
            //returns views with the needed model"all departments"

            var departments = await _services.GetAllDepartmentsAsync();
            return View(departments);

        }
        [HttpGet] //get:department/create
        public IActionResult Create()
        {
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmetViewModel departmetViewModel)
        {
            if (!ModelState.IsValid) return View(departmetViewModel);

            var departmentcreated = _mapper.Map<DepartmetViewModel, CreateDepartmentDto>(departmetViewModel);

           /* var res = _services.CreateDepartment(new CreateDepartmentDto
            {
                Code = departmetViewModel.Code,
                CreationDate = departmetViewModel.CreationDate,
                Name = departmetViewModel.Name,
                Description = departmetViewModel.Description
            });*/
           var res = await _services.CreateDepartmentAsync(departmentcreated);
            if (res > 0) return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Department is not created");
            return View(departmetViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = await _services.GetDepartmentByIdAsync(id.Value);


            if (department == null) { return NotFound(); };
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null )return BadRequest(); //400
            var department =await _services.GetDepartmentByIdAsync(id.Value);


            if (department == null) { return NotFound(); };

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmetViewModel>(department);

           /* return View(new DepartmetViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate

            });*/
           return View(departmentVM);

                                                                   
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,DepartmetViewModel departmetEditViewModel)
        {
            if (!ModelState.IsValid) return View(departmetEditViewModel);
            var msg = string.Empty;
            try
            {
                var department = _mapper.Map<UpdateDepartmentDto>(departmetEditViewModel);
                // var department = new UpdateDepartmentDto() { Id = id, Code = departmetEditViewModel.Code, Name = departmetEditViewModel.Name, Description = departmetEditViewModel.Description, CreationDate = departmetEditViewModel.CreationDate };
                department.Id = id;
                var x = await _services.UpdateDepartmentAsync(department);
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
        public async  Task<IActionResult> Delete(int? id)
        {
            if(!id.HasValue) return BadRequest();

            var department = await _services.GetDepartmentByIdAsync(id.Value);

            if (department == null) { return NotFound(); }
            return View(department);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult>Delete([FromRoute]int id)
        {
            var msg = string.Empty;
            try
            {
               var deleted = await _services.DeltedDepartmentAsync(id);
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
