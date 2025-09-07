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
        IEnumerable<EmpDto> GetAllEmpolyees();
        EmpDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreateEmpDto createEmpDto);
        int UpdateEmployee(UpdateEmpDto updateEmpDto);

        bool DeltedEmployee(int id);
    }
}
