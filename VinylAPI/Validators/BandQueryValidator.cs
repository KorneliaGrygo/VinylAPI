using FluentValidation;
using System.Linq;
using VinylAPI.Entities;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class BandQueryValidator : AbstractValidator<BandQuery>
    {
        private int[] allowedPageSizes = new[] { 20, 50, 100 };
        private string[] allowedSortByColumnNames = { nameof(Band.Name), nameof(Band.Description)};

        public BandQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(x => x.SortBy)
               .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
               .WithMessage($"SortBy jest opcjonalne lub musi zawierać wartości  {string.Join(",", allowedSortByColumnNames)}");
        }
    }
}
