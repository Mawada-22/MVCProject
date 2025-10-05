using Demo.BLL.Common.Sevices;
using Demo.BLL.Dtos;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Entites.Employees;
using Demo.DAL.Presistance.Repostries.EmployeeRepos;
using Demo.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IAtttachmentServices _atttachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork, IAtttachmentServices atttachmentServices)
        { _unitofwork = unitOfWork; _atttachmentServices = atttachmentServices; }


        public int CreateEmployee(CreateEmpDto createEmpDto)
        {
            var employee = new Employee()
            {
                Address = createEmpDto.Address,
                ID = createEmpDto.Id,
                Name = createEmpDto.Name,
                Email = createEmpDto.Email,
                PhoneNumber = createEmpDto.PhoneNumber,
                gender = createEmpDto.gender,
                Age = createEmpDto.Age,
                Salary = createEmpDto.Salary,
                HiringDate = createEmpDto.HiringDate,
                IsActive = createEmpDto.IsActive,
                EmpType = createEmpDto.EmpType,
                Departmentid = createEmpDto.Departmentid,

               

            };

            if (createEmpDto.Image is not null)
            {
                employee.Image = _atttachmentServices.Upload(createEmpDto.Image, "Images");
            }
            
                _unitofwork.employeeRepostiry.Add(employee);
            return _unitofwork.Compelete();

        }

        public bool DeltedEmployee(int id)
        {
            var repo = _unitofwork.employeeRepostiry;
            var emp = repo.Get(id);
            if (emp != null) repo.Delete(emp);
            return _unitofwork.Compelete() > 0;
        }

        public IEnumerable<EmpDto> GetEmpolyees(string sreach)
        {
            var Emps = _unitofwork.employeeRepostiry.GetQueryable().Where(E=>!E.IsDeleted && (string.IsNullOrEmpty(sreach) ||E.Name.ToLower().Contains(sreach.ToLower()))).Include(E=>E.department).Select(Emp => new EmpDto
            {
                ID = Emp.ID,
                Name = Emp.Name,
                Email = Emp.Email,
                Age = Emp.Age,
                Salary = Emp.Salary,
                gender = Emp.gender,
                EmpType = Emp.EmpType,
                DepartmentId = Emp.Departmentid,
                DepartmentName=Emp.department.Name,
                Image = Emp.Image
               


            });
            return Emps;
        }

        public EmpDetailsDto? GetEmployeeById(int id)
        {
            var Emp = _unitofwork.employeeRepostiry.Get(id);

            if (Emp is not null)
            {
                return new EmpDetailsDto()
                {
                    ID = Emp.ID,
                    Name = Emp.Name,
                    Email = Emp.Email,
                    Age = Emp.Age,
                    Salary = Emp.Salary,
                    gender = Emp.gender,
                    EmpType = Emp.EmpType,
                    LastUpdatedBy = Emp.LastUpdatedBy,
                    LastUpdatedOn = Emp.LastUpdatedOn,
                    CreatedBy = Emp.CreatedBy,
                    CreatedOn = Emp.CreatedOn,
                    PhoneNumber = Emp.PhoneNumber,
                    Departmentid = Emp.Departmentid,
                    DepartmentName = Emp.department != null ? Emp.department.Name : "No Department",
                    Image = Emp.Image


                };
            }
            return null;
        }

        public int UpdateEmployee(UpdateEmpDto updateEmpDto)
        {
            var emp = _unitofwork.employeeRepostiry.Get(updateEmpDto.Id);
            if (emp == null) return 0;

            // Update only the editable fields
            emp.Name = updateEmpDto.Name;
            emp.Age = updateEmpDto.Age;
            emp.Salary = updateEmpDto.Salary;
            emp.EmpType = updateEmpDto.EmpType;
            emp.Departmentid = updateEmpDto.DepartmentId;
            emp.gender = updateEmpDto.gender;
            emp.Address = updateEmpDto.Address;
           

            // Keep existing email and phone unless explicitly passed
            if (!string.IsNullOrWhiteSpace(updateEmpDto.Email))
                emp.Email = updateEmpDto.Email;

            if (!string.IsNullOrWhiteSpace(updateEmpDto.phonenumber))
                emp.PhoneNumber = updateEmpDto.phonenumber;

            if (!string.IsNullOrWhiteSpace(updateEmpDto.Address))
                emp.Address = updateEmpDto.Address;

            if (updateEmpDto.Image is not null)
            {
                emp.Image = _atttachmentServices.Upload(updateEmpDto.Image, "Images");
            }


            _unitofwork.employeeRepostiry.update(emp);
            return _unitofwork.Compelete();
        }

    }
}
