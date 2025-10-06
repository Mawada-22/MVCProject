using Demo.BLL.Dtos;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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
        public async Task<int> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            var department = new Department()
            {
                Code = createDepartmentDto.Code,Name = createDepartmentDto.Name,Description = createDepartmentDto.Description,
                CreationDate = createDepartmentDto.CreationDate, CreatedBy =1
            };
            
             _unitOfWork.departmentRepostiry.Add(department);
            return await _unitOfWork.CompeleteAsync();
        }


        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync()
        {
            var departments =await _unitOfWork.departmentRepostiry.GetQueryable().Select(department => new DepartmentToReturnDto
            {
                Id = department.ID,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,

            }).AsNoTracking().ToListAsync();
            return departments;

        }

        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.departmentRepostiry.GetAsync(id);

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

        public async Task<bool> DeltedDepartmentAsync(int id)
        {
            var repo = _unitOfWork.departmentRepostiry;
            var department = await  repo.GetAsync(id);
            if (department is not null)  repo.Delete(department);

            return  await _unitOfWork.CompeleteAsync() > 0;
        }

        public async Task<int> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
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
            return await _unitOfWork.CompeleteAsync();
           
            
        }
    }
}
