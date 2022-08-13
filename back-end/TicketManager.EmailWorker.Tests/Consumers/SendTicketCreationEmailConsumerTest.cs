using FluentEmail.Core;
using FluentEmail.Core.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using TicketManager.Contracts;
using TicketManager.Contracts.Entities;
using TicketManager.EmailWorker.Consumers;

namespace TicketManager.EmailWorker.Tests.Consumers
{
    public class SendTicketCreationEmailConsumerTest
    {
        private readonly SendTicketCreationEmailConsumer _sut;
        private readonly Mock<ILogger<SendTicketCreationEmailConsumer>> _logger;
        private readonly Mock<IFluentEmail> _fluentEmail;

        public SendTicketCreationEmailConsumerTest()
        {
            _logger = new Mock<ILogger<SendTicketCreationEmailConsumer>>();
            _fluentEmail = new Mock<IFluentEmail>();
            _sut = new SendTicketCreationEmailConsumer(_logger.Object, _fluentEmail.Object);
        }

        [Fact]
        public async Task Consume_ValidScenario_CallFluentEmailToMethod()
        {
            var message = new SendTicketCreationEmail { 
                TicketId = 1,
                Author = new TicketAuthor
                {
                    Id = 1,
                    Name = "john",
                    Email = "john@email.com"
                }
            };

            var context = Mock.Of<ConsumeContext<SendTicketCreationEmail>>(x => x.Message == message);

            await _sut.Consume(context);

            _fluentEmail.Verify(x => x.To(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Consume_ValidScenario_CallEmailSendAsync()
        {
            var message = new SendTicketCreationEmail {
                TicketId = 1,
                Author = new TicketAuthor
                {
                    Id = 1,
                    Name = "john",
                    Email = "john@email.com"
                }
            };

            var context = Mock.Of<ConsumeContext<SendTicketCreationEmail>>(x => x.Message == message);

            _fluentEmail.Setup(m => m.To(It.IsAny<string>())).Returns(_fluentEmail.Object);
            _fluentEmail.Setup(m => m.Subject(It.IsAny<string>())).Returns(_fluentEmail.Object);
            _fluentEmail.Setup(m => m.UsingTemplateFromFile(
                It.IsAny<string>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<bool>())).Returns(_fluentEmail.Object);
            _fluentEmail.Setup(m => m.SendAsync(null)).ReturnsAsync(new SendResponse());

            await _sut.Consume(context);

            _fluentEmail.Verify(x => x.SendAsync(null), Times.Once);
        }
    }
}
