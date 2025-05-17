namespace BudgetManager.Application.Transactions.GetTransactionById;

public record GetTransactionByIdQuery(Guid Id) : IRequest<GetTransactionByIdResponse>;
