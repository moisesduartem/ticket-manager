using FluentValidation.TestHelper;
using TicketManager.Application.Validators;
using TicketManager.Shared.DTOs.Auth;
using TicketManager.Shared.DTOs.Tickets;
using Xunit;

namespace TicketManager.Application.Tests.Validators
{
    public class CreateTicketRequestValidatorTests
    {
        private CreateTicketRequestValidator _sut;

        public CreateTicketRequestValidatorTests()
        {
            _sut = new CreateTicketRequestValidator();
        }

        [Fact]
        public void CreateTicketRequest_ValidFields_DoNotThrowValidationErrors()
        {
            var request = new CreateTicketRequest { Title = "ticket title", Description = "", AuthorId = 1, CategoryId = 1 };
            var result = _sut.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
        [Fact]
        public void CreateTicketRequest_EmptyTitle_ShouldThrowValidationError()
        {
            var request = new CreateTicketRequest { Title = "", Description = "", AuthorId = 1, CategoryId = 1 };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }
        
        [Fact]
        public void CreateTicketRequest_NullTitle_ShouldThrowValidationError()
        {
            var request = new CreateTicketRequest { Description = "", AuthorId = 1, CategoryId = 1 };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }
        
        [Fact]
        public void CreateTicketRequest_EmptyCategoryId_ShouldThrowValidationError()
        {
            var request = new CreateTicketRequest { Title = "ticket title", Description = "", AuthorId = 1 };
            var result = _sut.TestValidate(request);
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}
