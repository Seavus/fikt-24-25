using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Categories.GetCategoryById;

internal class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    private IApplicationDbContext _context;

    public GetCategoryByIdHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryId = CategoryId.Create(request.Id);
        
        var query = await (from c in _context.Categories.AsNoTracking()
                           join u in _context.Users.AsNoTracking()
                           on c.UserId equals u.Id
                           where c.Id == categoryId
                           select new
                           {
                               Category = c,
                               User = u
                           }).FirstOrDefaultAsync(cancellationToken);

        if (query== null)
        {
            throw new NotFoundException("Category Not Found.");
        }

        return new GetCategoryByIdResponse
        {
            Id = query.Category.Id.Value,
            Name = query.Category.Name,
            User = new UserModel(
                query.User.Id.Value,
                query.User.FirstName,
                query.User.LastName,
                query.User.Email)
        };
    }
}