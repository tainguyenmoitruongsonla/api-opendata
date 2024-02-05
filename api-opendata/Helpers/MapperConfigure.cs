using AutoMapper;
using api_opendata.Data;
using api_opendata.Dto;

namespace api_opendata.Helpers
{
    public class MapperConfigure : Profile
    {
        public MapperConfigure()
        {

            //-------------Authenticatiion--------------------

            //Users
            CreateMap<AspNetUsers, UserDto>().ReverseMap();

            //Users Info
            CreateMap<AspNetUsers, UserInfoDto>().ForMember(dest => dest.Dashboards, opt =>
                {
                    opt.MapFrom((src, dest) => dest.Dashboards);
                }).ReverseMap();

            //Roles
            CreateMap<AspNetRoles, RoleDto>()
                .ForMember(dest => dest.Dashboards, opt =>
                {
                    opt.MapFrom((src, dest) => dest.Dashboards);
                }).ReverseMap();

            //Dashboards
            CreateMap<Dashboards, DashboardDto>()
                .ForMember(dest => dest.Functions, opt =>
                {
                    opt.MapFrom((src, dest) => dest.Functions);
                }).ReverseMap();

            //Permissions
            CreateMap<Permissions, PermissionDto>().ReverseMap();

            //Dashboard for Roles and Users
            CreateMap<UserDashboards, UserDashboardDto>().ReverseMap();
            CreateMap<RoleDashboards, RoleDashboardDto>().ReverseMap();

            //functions
            CreateMap<Functions, FunctionDto>().ReverseMap();

            //-------------Other mapper--------------------
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Repository, RepositoryDto>().ReverseMap();
            CreateMap<Staff, StaffDto>().ReverseMap();
            CreateMap<Staff_Document, Staff_DocumentDto>().ReverseMap();

        }
    }
}
