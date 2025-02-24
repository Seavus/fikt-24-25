using BudgetManager.Application.Users.RegisterUser;
using Microsoft.AspNetCore.Http;

namespace BudgetManager.Api.Controllers;

/// <summary>
/// Registers a new user.
/// </summary>
[ApiController]
[Route("api/account")]
[Tags("User Management")]
public class AccountController : BaseController
{
    public AccountController(IMapper mapper, ISender mediator) : base(mapper, mediator) { }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Register([FromBody] RegisterUserRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Invalid data request.");
        }
        try
        {
            var result = new RegisterUserResponse(Guid.NewGuid());
            return Created($"api/users/{result.Id}", result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred:  {ex.Message}");
        }
    }
}
