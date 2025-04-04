namespace BudgetManager.Application.Users.GetUserById;

public class GetUserByIdRequestValidator :AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}