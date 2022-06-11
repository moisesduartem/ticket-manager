using AutoMapper;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Services
{
    public class CategoriesService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tickets = await _categoriesRepository.FindAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(tickets);
        }
    }
}
