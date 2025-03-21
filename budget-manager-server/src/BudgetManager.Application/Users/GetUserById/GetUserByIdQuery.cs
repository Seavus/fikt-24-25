namespace BudgetManager.Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdResponse>;