using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {

            RuleFor(x => x.Surname)
                .NotNull()
                .Length(1, 10);

                RuleFor(x => x.FirstName)
                .NotNull()
                .Length(1, 10);

            RuleFor(x => x.Patronymic)
                .NotNull()
                .Length(1, 10);

        }
    }
}
