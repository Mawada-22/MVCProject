 using AutoMapper;
using Demo.BLL.Dtos;
using Demo.pl.Models.Departmets;
using Demo.pl.Models.Employees;

namespace Demo.pl.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile() 
        {
            #region Department
            //Map<DepartmentDetailsDto, DepartmetViewModel>
            CreateMap<DepartmentDetailsDto, DepartmetViewModel>();
            CreateMap<DepartmetViewModel,CreateDepartmentDto>().ReverseMap();
            CreateMap<DepartmetViewModel,UpdateDepartmentDto>().ReverseMap();

            #endregion

            #region Empolyee
            CreateMap<EmpDetailsDto, EmployeeModelView>();
            CreateMap<EmployeeModelView, CreateEmpDto>().ReverseMap();
            CreateMap<EmployeeModelView, UpdateEmpDto>()
                .ForMember(dest => dest.phonenumber, opt => opt.MapFrom(src => src.phonenumber))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentID))
                .ReverseMap();

            #endregion
        }

    }
}