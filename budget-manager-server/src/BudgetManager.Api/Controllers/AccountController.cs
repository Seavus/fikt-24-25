using BudgetManager.Application.Users.RegisterUser;
using BudgetManager.Application.Users.LoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/account")]
[Tags("User Management")]
public class AccountController : BaseController
{
    public AccountController(IMapper mapper, ISender mediator) : base(mapper, mediator) { }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    [HttpPost]
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

        var result = new RegisterUserResponse(Guid.NewGuid());
        return Created($"api/users/{result.Id}", result);
    }

    /// <summary>
    /// Returns token for authenticated users.
    /// </summary>
    [HttpPost("token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] LoginUserRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Invalid login request.");
        }

        if (request.Email == "admin" && request.Password == "admin")
        {
            var result = new LoginUserResponse("mocked-jwt-token");
            return Ok(result);
        }
        return Unauthorized("Invalid credentials.");
    }
}