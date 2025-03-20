namespace BudgetManager.Application.Users.GetUsers;
public class GetUsersRequestValidator : AbstractValidator<GetUsersRequest>
{
    public GetUsersRequestValidator()
    {
        RuleFor(x => x.PageIndex)
            .NotEmpty().WithMessage("Page number is required.")
            .GreaterThan(0).WithMessage("Page number must be greater than zero.");

        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage("Page size is required.")
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
