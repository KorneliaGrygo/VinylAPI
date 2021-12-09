using FluentValidation;
using System.Linq;
using VinylAPI.Models;

namespace VinylAPI.Validators
{
    public class QueryValidator : AbstractValidator<Query>
    {
        private int[] allowedPageSizes = new[] { 20, 50, 100 };
        public QueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
