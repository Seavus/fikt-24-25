using BudgetManager.Application.Categories.DeleteCategory;
using BudgetManager.Application.Transactions.CreateTransaction;
using BudgetManager.Application.Transactions.DeleteTransaction;
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
        return Created($"api/transactions/{response.TransactionId}", response);
    }

    /// <summary>
    /// Delete transaction.
    /// </summary>
    [HttpDelete("{transactionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    {
        var command = new DeleteTransactionCommand(transactionId);
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
