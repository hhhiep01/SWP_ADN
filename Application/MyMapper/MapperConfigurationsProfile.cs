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
using Application.Request.Sample;
using Application.Response.Sample;
using Application.Request;
using Application.Response;


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
                        .ToList()))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
            CreateMap<ServiceRequest, Service>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
            CreateMap<ServiceUpdateRequest, Service>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

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
                .ForMember(dest => dest.AppointmentStaffName, opt => opt.MapFrom(src => src.AppointmentStaff != null ? $"{src.AppointmentStaff.FirstName} {src.AppointmentStaff.LastName}" : null))
                .ForMember(dest => dest.Samples, opt => opt.MapFrom(src => src.Samples));

            CreateMap<TestOrder, TestOrderShortResponse>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : null));

            CreateMap<CreateTestOrderRequest, TestOrder>();
            CreateMap<UpdateTestOrderRequest, TestOrder>();
            CreateMap<UpdateTestOrderStatusRequest, TestOrder>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.TestOrderStatus));
            CreateMap<UpdateDeliveryKitStatusRequest, TestOrder>()
                .ForMember(dest => dest.DeliveryKitStatus, opt => opt.MapFrom(src => src.DeliveryKitStatus));


            CreateMap<Blog, BlogResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src =>
                    src.UserAccount != null ? $"{src.UserAccount.FirstName} {src.UserAccount.LastName}" : null));
            CreateMap<CreateBlogRequest, Blog>();
            CreateMap<UpdateBlogRequest, Blog>();

            // Sample mappings
            CreateMap<Sample, SampleResponse>()
                .ForMember(dest => dest.CollectorName,
                    opt => opt.MapFrom(src => src.Collector != null ?
                        $"{src.Collector.FirstName} {src.Collector.LastName}" : null))
                .ForMember(dest => dest.TestOrder,
                    opt => opt.MapFrom(src => src.TestOrder))
                .ForMember(dest => dest.Result,
                    opt => opt.MapFrom(src => src.Result));
               /* .ForMember(dest => dest.SampleMethod,
                    opt => opt.MapFrom(src => src.TestOrder.SampleMethod));*/
            CreateMap<SampleRequest, Sample>();

            CreateMap<Result, ResultResponse>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Sample != null && src.Sample.TestOrder != null && src.Sample.TestOrder.Service != null ? src.Sample.TestOrder.Service.Name : null))
                .ForMember(dest => dest.SampleMethodName, opt => opt.MapFrom(src => src.Sample != null && src.Sample.TestOrder != null && src.Sample.TestOrder.SampleMethod != null ? src.Sample.TestOrder.SampleMethod.Name : null));

            CreateMap<ResultRequest, Result>();
        }
    }
}
