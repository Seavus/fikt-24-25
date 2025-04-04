using BudgetManager.Application.Data;
using BudgetManager.Application.Services;

namespace BudgetManager.Application.Users.LoginUser;

public class LogInUserHandler : IRequestHandler<LoginUserQuery, LoginUserResponse>
{
    private IApplicationDbContext _dbcontext;
    private ITokenService _tokenService;

    public LogInUserHandler(IApplicationDbContext dbcontext, ITokenService tokenService)
    {
        _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbcontext.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null || !user.Password.Equals(request.Password))
        {
            throw new UnauthorizedAccessException("Invalid e-mail or password.");
        }
        var token = _tokenService.CreateToken(user.Id.Value, $"{user.FirstName}.{user.LastName}", user.Email);

        return new LoginUserResponse(token);
    }
}