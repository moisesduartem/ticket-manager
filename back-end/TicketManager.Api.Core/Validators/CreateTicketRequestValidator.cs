using FluentValidation;
using TicketManager.Api.Core.Requests;

namespace TicketManager.Api.Core.Validators
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
