using BudgetManager.Application.Transactions.GetTransactionById;
using BudgetManager.Application.Transactions.GetTransactionsByUser;
using BudgetManager.Domain.Models;
using AutoMapper;

namespace BudgetManager.Application.Transactions.Mappings;
public class TransactionCategoryResolver : IValueResolver<Transaction, GetTransactionByIdResponse, CategoryModel?>
{
    private readonly IMapper _mapper;

    public TransactionCategoryResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public CategoryModel? Resolve(Transaction source, GetTransactionByIdResponse destination, CategoryModel? destMember, ResolutionContext context)
    {
        var category = context.Items["category"] as Category;
        if (category == null)
        {
            return null;
        }
        return _mapper.Map<CategoryModel>(category);
    }
}
