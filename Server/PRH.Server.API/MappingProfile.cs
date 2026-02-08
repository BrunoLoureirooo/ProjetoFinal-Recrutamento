using AutoMapper;
using PRH.Server.Entities.Models;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Company, CompanyDto>()
            //    .ForMember(c => c.FullAddress,
            //        opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            //CreateMap<Company, CompanyDto>()
            //    .ForCtorParam("FullAddress",
            //        opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            //CreateMap<Company, CompanyDto>();
            //CreateMap<Employee, EmployeeDto>();
            //CreateMap<CompanyForCreationDto, Company>();
            //CreateMap<EmployeeForCreationDto, Employee>();
            //CreateMap<EmployeeForUpdateDto, Employee>();
            //CreateMap<CompanyForUpdateDto, Company>();
            //CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UserForRegistrationDto, ApplicationUser>();
            CreateMap<Empresa, EmpresaDto>();
        }
    }
}
