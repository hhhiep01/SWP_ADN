
using AutoMapper;
using Domain.Entity;
using Application.Response.UserAccount;
using Application.Request.User;


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

            

        }
    }
}
