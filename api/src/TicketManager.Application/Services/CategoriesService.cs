using AutoMapper;
using Microsoft.Extensions.Logging;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Services
{
    public class CategoriesService
    {
        private readonly ILogger<CategoriesService> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ILogger<CategoriesService> logger, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Calling the repository method to get all categories");
            var tickets = await _categoriesRepository.FindAllAsync(cancellationToken);

            _logger.LogInformation($"Mapping & returning results");
            return _mapper.Map<IEnumerable<CategoryViewModel>>(tickets);
        }
    }
}
