using BudgetManager.Domain.Models;
using MediatR;
using BudgetManager.Application.Data;
using BudgetManager.Domain.Models.ValueObjects;

namespace BudgetManager.Application.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                new UserId(Guid.NewGuid()),  
                request.Firstname,
                request.Lastname,
                request.Email,
                request.Password
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterUserResponse(user.Id.Value); 
        }
    }
}