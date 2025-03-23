using Demo.BLL.Dtos;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.DepartmentServicea
{
    public class DepartmentServisces : IDepartmentServices
    {

        private readonly IDepartmentRepostiry _departmentRepostiry;
       
        public DepartmentServisces(IDepartmentRepostiry departmentRepostiry)
            
            {
              _departmentRepostiry = departmentRepostiry;
            }
        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department()
            {
                Code = createDepartmentDto.Code,Name = createDepartmentDto.Name,Description = createDepartmentDto.Description,
                CreationDate = createDepartmentDto.CreationDate, CreatedBy =1
            };
            
            return _departmentRepostiry.Add(department);
        }


        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepostiry.GetQueryable().Select(department => new DepartmentToReturnDto
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
            var department = _departmentRepostiry.Get(id);

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
            var department = _departmentRepostiry.Get(id);
            if (department is not null)
            {
                return _departmentRepostiry.Delete(department)>0;
            }
            return false;
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

           
                return _departmentRepostiry.update(department);
           
            
        }
    }
}
