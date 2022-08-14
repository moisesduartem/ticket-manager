using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketManager.Api.Core.Domain.DTOs.Tickets;
using TicketManager.Api.Core.Repositories;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Core.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDTO>>
    {
        private readonly ILogger<GetAllCategoriesQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository _categoriesRepository;

        public GetAllCategoriesQueryHandler(ILogger<GetAllCategoriesQueryHandler> logger, IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all ticket categories");
            var categories = await _categoriesRepository.FindAllAsync(cancellationToken);

            if (!categories.Any())
            {
                _logger.LogInformation("Zero results, returning an empty list");
                return Enumerable.Empty<CategoryDTO>();
            }

            _logger.LogInformation("Mapping & returning categories, Total={Count}", categories.Count());
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    }
}
