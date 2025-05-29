using BudgetManager.Application.Categories.CreateCategory;
using BudgetManager.Application.Categories.DeleteCategory;
using BudgetManager.Application.Categories.GetCategoryById;
using BudgetManager.Application.Categories.UpdateCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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

        return Created($"api/categories/{response.Id}", response);
    }

    ///<summary>
    ///Updates an existing category.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
    {
        var command = Mapper.Map<UpdateCategoryCommand>(request);

        var response = await Mediator.Send(command);

        return Ok(response);
    }

    ///<summary>
    ///Deletes an existing category.
    /// </summary>
    [HttpDelete("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteCategoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand(id);

        var response = await Mediator.Send(command);
        
        return Ok(response);
    }

    ///<summary>
    ///Retrieves a category by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryByIdResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var query = new GetCategoryByIdQuery(id);

        var response = await Mediator.Send(query);
        
        return Ok(response);
    }
}