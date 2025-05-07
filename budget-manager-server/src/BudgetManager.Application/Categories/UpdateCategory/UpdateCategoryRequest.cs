namespace BudgetManager.Application.Categories.UpdateCategory;

public class UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
