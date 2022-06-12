using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using TicketManager.Application.Services;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Shared.DTOs.Tickets;
using Xunit;

namespace TicketManager.Application.Tests.Services
{
    public class CategoriesServiceTests
    {
        private readonly CategoriesService _sut;
        private readonly Mock<ILogger<CategoriesService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ICategoriesRepository> _categoriesRepository;

        public CategoriesServiceTests()
        {
            _logger = new Mock<ILogger<CategoriesService>>();
            _mapper = new Mock<IMapper>();
            _categoriesRepository = new Mock<ICategoriesRepository>();
            _sut = new CategoriesService(_logger.Object, _mapper.Object, _categoriesRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_OnExecuting_CallFindAllAsync()
        {
            await _sut.GetAllAsync(default);
            _categoriesRepository.Verify(x => x.FindAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public async Task GetAllAsync_OnExecuting_MapResults()
        {
            var mockedResults = new List<Category> { new Category("category name") }.AsEnumerable();
            _categoriesRepository.Setup(x => x.FindAllAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockedResults));
            
            await _sut.GetAllAsync(default);

            _mapper.Verify(x => x.Map<IEnumerable<CategoryViewModel>>(It.IsAny<IEnumerable<Category>>()), Times.Once);
        }
    }
}
