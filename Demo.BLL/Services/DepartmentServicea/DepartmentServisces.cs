using Demo.BLL.Dtos;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.DepartmentServicea
{
    public class DepartmentServisces : IDepartmentServices
    {

        private readonly IUnitOfWork _unitOfWork;
       
        public DepartmentServisces(IUnitOfWork unitOfWork)
            
            {
            _unitOfWork = unitOfWork;
            }
        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department()
            {
                Code = createDepartmentDto.Code,Name = createDepartmentDto.Name,Description = createDepartmentDto.Description,
                CreationDate = createDepartmentDto.CreationDate, CreatedBy =1
            };
            
             _unitOfWork.departmentRepostiry.Add(department);
            return _unitOfWork.Compelete();
        }


        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _unitOfWork.departmentRepostiry.GetQueryable().Select(department => new DepartmentToReturnDto
            {
                Id = department.ID,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,

            });
            return departments;

        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.departmentRepostiry.Get(id);

            if (department is not  null) {
                return new DepartmentDetailsDto
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    Description = department.Description,
                    LastUpdatedBy = department.LastUpdatedBy,
                    LastUpdatedOn = department.LastUpdatedOn
                };
            }
            return null;

        }

        public bool DeltedDepartment(int id)
        {
            var repo = _unitOfWork.departmentRepostiry;
            var department = repo.Get(id);
            if (department is not null)  repo.Delete(department);

            return _unitOfWork.Compelete() > 0;
        }

        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {

            var department = new Department()
            {
                ID = updateDepartmentDto.Id,
                Name = updateDepartmentDto.Name,
                Code = updateDepartmentDto.Code,
                Description = updateDepartmentDto.Description,
                CreatedBy = 1,
                CreationDate = updateDepartmentDto.CreationDate,
                LastUpdatedBy= 1,   
                LastUpdatedOn = DateTime.Now,

            };

           
              _unitOfWork.departmentRepostiry.update(department);
            return _unitOfWork.Compelete();
           
            
        }
    }
}
