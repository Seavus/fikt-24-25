﻿using BudgetManager.Application.Users.RegisterUser;
using BudgetManager.Application.Users.LoginUser;
using BudgetManager.Application.Users.DeleteUser;
using BudgetManager.Application.Users.UpdateUser;
using BudgetManager.Domain.Models.ValueObjects;

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
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
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
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var query = _mapper.Map<LoginUserQuery>(request);

        var response = await mediator.Send(query);
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
        var command = _mapper.Map<DeleteUserCommand>(id);
        var result = await _mediator.Send(command);

        return Ok(result);
    }
    /// Updates an existing user's first name and last name.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
