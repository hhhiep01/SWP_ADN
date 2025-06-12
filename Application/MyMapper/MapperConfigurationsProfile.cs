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
using Application.Request.TestOrder;
using Application.Response.TestOrder;
using Application.Request.Blog;
using Application.Response.Blog;


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


            // TestOrder mappings
            CreateMap<TestOrder, TestOrderResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : null))
                    .ForMember(dest => dest.Services,
                                         opt => opt.MapFrom(src => src.Service))
               .ForMember(dest => dest.SampleMethods,
                                        opt => opt.MapFrom(src => src.SampleMethod))
                .ForMember(dest => dest.AppointmentStaffName, opt => opt.MapFrom(src => src.AppointmentStaff != null ? $"{src.AppointmentStaff.FirstName} {src.AppointmentStaff.LastName}" : null));

            CreateMap<CreateTestOrderRequest, TestOrder>();
            CreateMap<UpdateTestOrderRequest, TestOrder>();
            CreateMap<UpdateTestOrderStatusRequest, TestOrder>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TestOrderStatus));
            CreateMap<UpdateDeliveryKitStatusRequest, TestOrder>()
                .ForMember(dest => dest.DeliveryKitStatus, opt => opt.MapFrom(src => src.DeliveryKitStatus));
            CreateMap<UpdateAppointmentStatusRequest, TestOrder>()
                .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => src.AppointmentStatus));


            CreateMap<Blog, BlogResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                    src.UserAccount != null ? $"{src.UserAccount.FirstName} {src.UserAccount.LastName}" : null));
            CreateMap<CreateBlogRequest, Blog>();
            CreateMap<UpdateBlogRequest, Blog>();

        }
    }
}
