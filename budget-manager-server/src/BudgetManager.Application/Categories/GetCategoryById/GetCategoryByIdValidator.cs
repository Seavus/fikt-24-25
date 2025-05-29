namespace BudgetManager.Application.Categories.GetCategoryById;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Is Required");
    }
}