using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Categories.CreateCategory;

internal class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private IApplicationDbContext _context;
    private ICurrentUser _currentUser;

    public CreateCategoryHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(_currentUser.UserId!.Value);

        var category = Category.Create(
            CategoryId.Create(Guid.NewGuid()),
            request.Name,
            userId
            );

        _context.Categories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCategoryResponse(category.Id.Value);
    }
}
