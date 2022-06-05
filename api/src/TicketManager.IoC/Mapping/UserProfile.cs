using AutoMapper;
using TicketManager.Domain.Entities;
using TicketManager.Shared.DTOs.Auth;

namespace TicketManager.IoC.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AuthUserViewModel>();
        }
    }
}
