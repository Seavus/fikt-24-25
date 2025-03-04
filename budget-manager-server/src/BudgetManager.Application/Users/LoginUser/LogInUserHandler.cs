using BudgetManager.Application.Data;
using BudgetManager.Application.Services;
using BudgetManager.Domain.Models;

namespace BudgetManager.Application.Users.LoginUser;

public class LogInUserHandler : IRequestHandler<LoginUserQuery, LoginUserResponse>
{
    private IApplicationDbContext _dbcontext;
    private ITokenService _tokenService;

    public LogInUserHandler(IApplicationDbContext dbcontext, ITokenService tokenService)
    {
        _dbcontext = dbcontext;
        _tokenService = tokenService;
    }

    public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbcontext.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null || !VerifyPassword(user, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid e-mail or password.");
        }
        var token = _tokenService.CreateToken(user.Id.Value, user.Email, user.FirstName);

        return new LoginUserResponse(token);
    }

    private bool VerifyPassword(User user, string password)
    {
        return user.Password == password;
    }
}

