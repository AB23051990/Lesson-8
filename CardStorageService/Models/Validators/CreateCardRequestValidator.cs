using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        public CreateCardRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 255);

        }
    }
}
