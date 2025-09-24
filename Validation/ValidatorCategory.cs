using FluentValidation;
using pos_simple.Model;

namespace pos_simple.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
        }
    }
}
