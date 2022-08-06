using AutoMapper;
using TicketManager.Domain.Entities;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.IoC.Mapping
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<CreateTicketRequest, Ticket>();

            CreateMap<Ticket, TicketViewModel>();
            CreateMap<User, AuthorViewModel>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
