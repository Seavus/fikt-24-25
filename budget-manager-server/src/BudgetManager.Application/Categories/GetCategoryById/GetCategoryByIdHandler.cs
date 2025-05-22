using BudgetManager.Application.Data;
using BudgetManager.Application.Exceptions;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Categories.GetCategoryById;

internal class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetCategoryByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryId = CategoryId.Create(request.Id);

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException("Category Not Found.");
        }
        
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == category.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User For Category Not Found.");
        }

        var response = new GetCategoryByIdResponse
        {
            Id = category.Id.Value,
            Name = category.Name,
            User = new UserModel(
                user.Id.Value,
                user.FirstName,
                user.LastName,
                user.Email)
        };
        return response;
    }
}