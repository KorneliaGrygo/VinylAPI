using FluentValidation;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class CreateBandDtoValidator : AbstractValidator<CreateBandDto>
    {
        public CreateBandDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
