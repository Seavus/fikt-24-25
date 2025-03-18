namespace BudgetManager.Application.Users.UpdateUser;
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .WithMessage("First name is required.")
        .MaximumLength(50)
        .WithMessage("First name must be at maximum 50 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithMessage("Last name must be at maximum 50 characters long.");
    }
}
