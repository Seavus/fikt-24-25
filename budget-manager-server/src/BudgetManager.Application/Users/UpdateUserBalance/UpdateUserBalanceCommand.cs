using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.UpdateUserBalance;
public record UpdateUserBalanceCommand(decimal Balance) : IRequest<UpdateUserBalanceResponse>;