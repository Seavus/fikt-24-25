namespace BudgetManager.Application.Users.GetUserById;

public record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public DateTime CreatedOn { get; init; }
    public string CreatedBy { get; init; }
    public DateTime? UpdatedOn { get; init; }
    public string? UpdatedBy { get; init; }

    public GetUserByIdResponse() { }
}