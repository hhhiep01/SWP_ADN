
using AutoMapper;
using Domain.Entity;
using Application.Response.UserAccount;
using Application.Request.User;
using Application.Request.Service;
using Application.Response.Service;
using Application.Request.Role;
using Application.Request.SampleMethod;
using Application.Request.SampleMethodService;
using Application.Response.Role;
using Application.Response.SampleMethod;


namespace Application.MyMapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {

            //UserAccount
            //CreateMap<UserProfileResponse, UserAccount>();
            CreateMap<UpdateUserRequest, UserAccount>();
            CreateMap<UserAccount, UserProfileResponse>();
            CreateMap<UserAccount, AccountResponse>();

            // Role mappings
            CreateMap<Role, RoleResponse>();
            CreateMap<RoleRequest, Role>();
            CreateMap<RoleUpdateRequest, Role>();

            // Service mappings
            CreateMap<Service, ServiceResponse>()
                .ForMember(
                    dest => dest.SampleMethods,
                    opt => opt.MapFrom(src => src.ServiceSampleMethods
                        .Select(sms => sms.SampleMethod)
                        .ToList()));
            CreateMap<ServiceRequest, Service>();
            CreateMap<ServiceUpdateRequest, Service>();

            // SampleMethod mappings
            CreateMap<SampleMethod, SampleMethodResponse>();
            CreateMap<SampleMethodRequest, SampleMethod>();
            CreateMap<SampleMethodUpdateRequest, SampleMethod>();

            // SampleMethodService mappings
            CreateMap<SampleMethodServiceUpdateRequest, ServiceSampleMethod>();



        }
    }
}
