using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Users.GetUsers;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/account")]
[Tags("User Management")]
public class AccountController : BaseController
{
    public AccountController(IMapper mapper, ISender mediator) : base(mapper, mediator)
    {
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RegisterUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = Mapper.Map<RegisterUserCommand>(request);

        var result = await Mediator.Send(command);

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
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var query = Mapper.Map<LoginUserQuery>(request);

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    [HttpDelete("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);

        var result = await Mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing user's first name and last name.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var command = Mapper.Map<UpdateUserCommand>(request);

        var response = await Mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of users.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<GetUsersResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var query = Mapper.Map<GetUsersQuery>(request);

        var response = await Mediator.Send(query);

        return Ok(response);
    }
}
