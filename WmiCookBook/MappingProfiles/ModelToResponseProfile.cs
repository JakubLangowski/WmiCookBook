using AutoMapper;
using WmiCookBook.Contracts.Response.User;
using WmiCookBook.Models;

namespace WmiCookBook.MappingProfiles
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}