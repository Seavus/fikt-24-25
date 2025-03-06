using MediatR;

namespace BudgetManager.Application.Users.RegisterUser;
    public record RegisterUserCommand(string Firstname, string Lastname, string Email, string Password) : IRequest<RegisterUserResponse>;