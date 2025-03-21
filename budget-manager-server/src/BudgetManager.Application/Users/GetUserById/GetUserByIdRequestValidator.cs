namespace BudgetManager.Application.Users.GetUserById;

public class GetUserByIdRequestValidator :AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}