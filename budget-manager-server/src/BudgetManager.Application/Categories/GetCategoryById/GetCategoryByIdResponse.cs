namespace BudgetManager.Application.Categories.GetCategoryById;

public record GetCategoryByIdResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }

    public GetCategoryByIdResponse()
    {

    }
}