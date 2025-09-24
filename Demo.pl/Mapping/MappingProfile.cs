using AutoMapper;
using Demo.BLL.Dtos;
using Demo.pl.Models.Departmets;

namespace Demo.pl.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile() {
            #region Department
            //Map<DepartmentDetailsDto, DepartmetViewModel>
            CreateMap<DepartmentDetailsDto, DepartmetViewModel>();
            CreateMap<DepartmetViewModel,CreateDepartmentDto>().ReverseMap();
            CreateMap<DepartmetViewModel,UpdateDepartmentDto>().ReverseMap();

        #endregion  
        }
         
    }
}