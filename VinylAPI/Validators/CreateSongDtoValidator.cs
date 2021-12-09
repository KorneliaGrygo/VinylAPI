using FluentValidation;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class CreateSongDtoValidator : AbstractValidator<CreateSongDto>
    {
        public CreateSongDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Lenght).NotNull().LessThanOrEqualTo(60.00);

        }
    }
}
