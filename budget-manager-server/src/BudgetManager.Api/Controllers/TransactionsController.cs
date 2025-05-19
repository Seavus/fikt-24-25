using BudgetManager.Application.Categories.DeleteCategory;
using BudgetManager.Application.Transactions.CreateTransaction;
using BudgetManager.Application.Transactions.DeleteTransaction;
using BudgetManager.Application.Transactions.GetTransactionById;
using Microsoft.AspNetCore.Authorization;

namespace BudgetManager.Api.Controllers;

[ApiController]
[Route("api/transactions")]
[Tags("Transactions Management")]
[Authorize]
public class TransactionsController : BaseController
{
    public TransactionsController(IMapper mapper, ISender mediator) : base(mapper, mediator)
    {
    }

    /// <summary>
    /// Creates a new transaction.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateTransactionCommand>(request);

        var response = await Mediator.Send(command);
        return Created($"api/transactions/{response.Id}", response);
    }

    /// <summary>
    /// Delete transaction.
    /// </summary>
    [HttpDelete("{Id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid Id)
    {
        var command = new DeleteTransactionCommand(Id);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get transaction by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTransactionByIdResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        var query = new GetTransactionByIdQuery(id);
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}
