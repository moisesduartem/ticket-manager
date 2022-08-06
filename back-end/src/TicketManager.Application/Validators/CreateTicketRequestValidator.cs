using FluentValidation;
using TicketManager.Shared.DTOs.Tickets;

namespace TicketManager.Application.Validators
{
    public class CreateTicketRequestValidator : AbstractValidator<CreateTicketRequest>
    {
        public CreateTicketRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(12);
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
