namespace BudgetManager.Application.Users.GetCatogiresByUser;

public class GetCategoriesRequest
{
    [FromQuery]
    public int PageIndex { get; set; } = 1;

    [FromQuery]
    public int PageSize { get; set; } = 10;
}
