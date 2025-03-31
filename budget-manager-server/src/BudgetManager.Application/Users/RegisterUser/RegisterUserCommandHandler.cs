using BudgetManager.Domain.Models;
using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IApplicationDbContext _context;

    public RegisterUserCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            UserId.Create(Guid.NewGuid()),
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            false
        );

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse(user.Id.Value);
    }

}