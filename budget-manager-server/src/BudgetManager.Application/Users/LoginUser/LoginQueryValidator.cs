namespace BudgetManager.Application.Users.LoginUser;
public class LoginQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(4)
            .WithMessage("Password must be at least 4 characters long."); ;
    }
}