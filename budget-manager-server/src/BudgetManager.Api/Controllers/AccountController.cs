using BudgetManager.Application.Users.RegisterUser;
using BudgetManager.Application.Users.LoginUser;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/account")]
[Tags("User Management")]
public class AccountController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;
    public AccountController(IMapper mapper, ISender mediator) : base(mapper, mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async  Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
            var command = _mapper.Map<RegisterUserCommand>(request);
            var result = await _mediator.Send(command);

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