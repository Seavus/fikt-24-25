using BudgetManager.Application.Categories.GetCatogiresByUser;
using BudgetManager.Application.Common.Responses;
using BudgetManager.Application.Users.GetUserById;
using BudgetManager.Application.Users.GetUsers;
using BudgetManager.Application.Users.VerifyEmail;
using Microsoft.AspNetCore.Authorization;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/account")]
[Tags("User Management")]
[Authorize]
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
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var query = Mapper.Map<GetUsersQuery>(request);

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByIdResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var query = new GetUserByIdQuery(id);

        var response = await Mediator.Send(query);

        return Ok(response);
    }

    ///<summary>
    ///Verifies a users email using token.
    /// </summary>
    [HttpGet("{userId:guid}/verify-email/{token:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VerifyEmailResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyEmail([FromRoute] Guid userId, [FromRoute] Guid token)
    {
        var query = new VerifyEmailQuery(userId, token);

        var result = await Mediator.Send(query);

        return Ok(result);
    }

    ///<summary>
    ///Retrieves a category by user.
    /// </summary>
    [HttpGet("categories")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<GetCategoriesByUserResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoriesByUser(
        [FromQuery] GetCategoriesRequest request)
        
    {
        var query = Mapper.Map<GetCategoriesByUserQuery>(request);

        var result = await Mediator.Send(query);

        return Ok(result);
    }
}