using FluentValidation;
using System.Linq;
using VinylAPI.Data;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(VinylAPIDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password).WithMessage("Hasła nie są identyczne");
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Ten email jest zajęty");
                }
            });
        }
    }
}
