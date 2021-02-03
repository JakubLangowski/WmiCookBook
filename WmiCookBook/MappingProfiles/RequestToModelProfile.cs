using AutoMapper;
using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Contracts.Request.Queries;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;

namespace WmiCookBook.MappingProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            
            CreateMap<AuthRegisterRequest, User>();
            CreateMap<AuthChangePasswordRequest, User>();
        }
    }
}