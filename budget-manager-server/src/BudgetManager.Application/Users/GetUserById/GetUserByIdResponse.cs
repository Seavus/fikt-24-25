namespace BudgetManager.Application.Users.GetUserById;

public record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required decimal Balance { get; init; }
    public GetUserByIdResponse() { }
}