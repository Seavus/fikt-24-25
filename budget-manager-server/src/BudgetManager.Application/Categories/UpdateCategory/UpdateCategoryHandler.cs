using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Categories.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private IApplicationDbContext _context;

    public UpdateCategoryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var categoryId = CategoryId.Create(request.CategoryId);

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }

        category.Update(request.Name);

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryResponse(category.Id.Value, category.Name);
    }
}