using AutoMapper;
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
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITicketsRepository> _ticketsRepository;

        public TicketsServiceTests()
        {
            _mapper = new Mock<IMapper>();
            _ticketsRepository = new Mock<ITicketsRepository>();
            _sut = new TicketsService(_mapper.Object, _ticketsRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_OnExecuting_CallFindAllAsync()
        {
            await _sut.GetAllAsync();
            _ticketsRepository.Verify(x => x.FindAllAsync(), Times.Once);
        }
        
        [Fact]
        public async Task GetAllAsync_OnExecuting_MapResults()
        {
            var mockedResults = new List<Ticket> { new Ticket("title", 1, 1) }.AsEnumerable();
            _ticketsRepository.Setup(x => x.FindAllAsync()).Returns(Task.FromResult(mockedResults));
            
            await _sut.GetAllAsync();

            _mapper.Verify(x => x.Map<IEnumerable<TicketViewModel>>(It.IsAny<IEnumerable<Ticket>>()), Times.Once);
        }

    }
}
