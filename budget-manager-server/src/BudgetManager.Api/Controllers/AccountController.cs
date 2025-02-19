using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BudgetManager.Application.Users.RegisterUser;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : BaseController
{
    public AccountController(IMapper mapper, ISender mediator) : base(mapper, mediator) { }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
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
            var response = new RegisterUserResponse(Guid.NewGuid());
            return StatusCode(201, response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred:  {ex.Message}");
        }
    }
}