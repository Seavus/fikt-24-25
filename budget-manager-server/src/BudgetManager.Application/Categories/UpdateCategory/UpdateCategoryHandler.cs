using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Categories.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private IApplicationDbContext _context;

    public UpdateCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var categoryId = new CategoryId(request.Id);


        var category = await _context.Categories.FindAsync(categoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException("Category not found.");
        }

        category.Update(request.Name);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}