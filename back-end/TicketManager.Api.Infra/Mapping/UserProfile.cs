using AutoMapper;
using TicketManager.Api.Core.Domain.DTOs.Auth;
using TicketManager.Api.Core.Domain.Entities;

namespace TicketManager.IoC.Mapping
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AuthUserDTO>();
        }
    }
}
