using Demo.BLL.Dtos;
using Demo.DAL.Entites.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.DepartmentServicea
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int CreateDepartment( CreateDepartmentDto createDepartmentDto);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);

        bool DeltedDepartment (int id);
        
    }
}
