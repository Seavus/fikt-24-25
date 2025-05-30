using BudgetManager.Application.Data;
using BudgetManager.Application.Transactions.GetTransactionById;
using BudgetManager.Application.Transactions.GetTransactionsByUser;
using BudgetManager.Domain.Models;

namespace BudgetManager.Application.Transactions.Mappings;
public class TransactionCategoryResolver : IValueResolver<Transaction, GetTransactionByIdResponse, CategoryModel>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public TransactionCategoryResolver(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public CategoryModel Resolve(Transaction source, GetTransactionByIdResponse destination, CategoryModel destMember, ResolutionContext context)
    {
        var category = context.Items["category"] as Category ?? throw new AutoMapperMappingException("Category not found in context items"); 
        return _mapper.Map<CategoryModel>(category!);
    }
}
