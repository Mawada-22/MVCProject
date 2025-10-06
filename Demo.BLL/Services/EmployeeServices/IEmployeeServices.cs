using Demo.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmpDto>> GetEmpolyeesAsync(string search);
        Task<EmpDetailsDto?> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreateEmpDto createEmpDto);
        Task<int> UpdateEmployeeAsync(UpdateEmpDto updateEmpDto);

        Task<bool> DeltedEmployeeAsync(int id);
    }
}
