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
    public class TicketsServiceTests
    {
        private readonly TicketsService _sut;
        private readonly Mock<ILogger<TicketsService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITicketsRepository> _ticketsRepository;

        public TicketsServiceTests()
        {
            _logger = new Mock<ILogger<TicketsService>>();
            _mapper = new Mock<IMapper>();
            _ticketsRepository = new Mock<ITicketsRepository>();
            _sut = new TicketsService(_logger.Object, _mapper.Object, _ticketsRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_OnExecuting_CallFindAllAsync()
        {
            await _sut.GetAllAsync(default);
            _ticketsRepository.Verify(x => x.FindAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Fact]
        public async Task GetAllAsync_OnExecuting_MapResults()
        {
            var mockedResults = new List<Ticket> { new Ticket("title", 1, 1) }.AsEnumerable();
            _ticketsRepository.Setup(x => x.FindAllAsync(It.IsAny<CancellationToken>()))
                              .Returns(Task.FromResult(mockedResults));
            
            await _sut.GetAllAsync(default);

            _mapper.Verify(x => x.Map<IEnumerable<TicketViewModel>>(It.IsAny<IEnumerable<Ticket>>()), Times.Once);
        }

        [Fact]
        public async Task CreateOneAsync_WithValidRequest_MapRequest()
        {
            var request = new CreateTicketRequest { Title = "ticket title", Description = "", AuthorId = 1, CategoryId = 1 };
            await _sut.CreateOneAsync(request, default);
            _mapper.Verify(x => x.Map<Ticket>(It.IsAny<CreateTicketRequest>()), Times.Once);
        }
        
        [Fact]
        public async Task CreateOneAsync_WithValidRequest_MapResults()
        {
            var request = new CreateTicketRequest { Title = "ticket title", Description = "", AuthorId = 1, CategoryId = 1 };
            var ticket = new Ticket("ticket title", 1, 1);
            _mapper.Setup(x => x.Map<Ticket>(It.IsAny<CreateTicketRequest>())).Returns(ticket);
            
            await _sut.CreateOneAsync(request, default);

            _ticketsRepository.Verify(x => x.CreateOneAsync(It.IsAny<Ticket>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
