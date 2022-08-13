using AutoMapper;
using TicketManager.Api.Core.Domain.DTOs.Tickets;
using TicketManager.Api.Core.Domain.Entities;
using TicketManager.Api.Core.Requests;

namespace TicketManager.IoC.Mapping
{
    internal class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<CreateTicketRequest, Ticket>();

            CreateMap<Ticket, TicketDTO>();
            CreateMap<User, AuthorDTO>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
