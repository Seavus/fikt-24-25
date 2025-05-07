using BudgetManager.Application.Categories.CreateCategory;
using Microsoft.AspNetCore.Authorization;

namespace BudgetManager.Api.Controllers;
[Authorize]
[Route("api/categories")]
[ApiController]
[Tags("Categories Management")]
public class CategoriesController : BaseController
{
    public CategoriesController(IMapper mapper, ISender mediator) : base(mapper, mediator)
    {
    }

    ///<summary>
    ///Creates a new category.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateCategoryCommand>(request);

        var response = await Mediator.Send(command, cancellationToken);
        
        return Created($"api/categories/{response.CategoryId}", response);
    }
}
