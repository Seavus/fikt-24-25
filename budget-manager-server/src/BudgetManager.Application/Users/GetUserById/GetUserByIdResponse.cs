namespace BudgetManager.Application.Users.GetUserById;

public record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public DateTime CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public DateTime? UpdatedOn { get; init; }
    public string? UpdatedBy { get; init; }

    public GetUserByIdResponse() { }
}