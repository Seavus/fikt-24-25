using System;

namespace BudgetManager.Application.Services;
    public interface ITokenService
    {
        string CreateToken(Guid id, string name, string email);
    }