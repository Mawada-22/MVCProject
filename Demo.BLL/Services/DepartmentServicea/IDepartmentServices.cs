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
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync( CreateDepartmentDto createDepartmentDto);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto);

        Task<bool> DeltedDepartmentAsync (int id);
        
    }
}
