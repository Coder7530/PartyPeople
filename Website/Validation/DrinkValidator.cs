using FluentValidation;
using Website.Models;

namespace Website.Validation
{
    /// <summary>
    /// The validator for <see cref="Drink"/> objects.
    /// </summary>
    public class DrinkValidator : AbstractValidator<Drink>
    {
        /// <summary>
        /// Instantiates a new instance of the validator.
        /// </summary>
        public DrinkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100)
                .WithMessage("The drink name is required and must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("The drink description must not exceed 255 characters.");
        }
    }
}
