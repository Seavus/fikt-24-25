﻿using AutoMapper.QueryableExtensions;
using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Categories.GetCatogiresByUser;

internal sealed class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, PaginatedResponse<GetCategoriesByUserResponse>>
{
    private IApplicationDbContext _context;
    private ICurrentUser _currentUser;
    private IMapper _mapper;

    public GetCategoriesByUserQueryHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PaginatedResponse<GetCategoriesByUserResponse>> Handle(GetCategoriesByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var query = _context.Categories
            .AsNoTracking()
            .AsEnumerable()
            .Where(c => c.UserId.Value == userId)
            .AsQueryable();

        int totalCount = query.Count();

        var categories = query
            .OrderBy(c => c.Name)
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var items = _mapper.Map<List<GetCategoriesByUserResponse>>(categories);

        return new PaginatedResponse<GetCategoriesByUserResponse>(items, request.PageIndex, request.PageSize, totalCount);

    }
}