﻿using BudgetManager.Application.Categories.CreateCategory;
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

        return Created($"api/categories/{response.CategoryId}", response);
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
}
