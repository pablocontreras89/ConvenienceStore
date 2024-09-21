using ConvenienceStore.API.DTOs;
using FluentValidation;

namespace ConvenienceStore.API.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.UPC)
                .Matches("^\\d+$")
                .NotEmpty()
                .MaximumLength(15);
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
