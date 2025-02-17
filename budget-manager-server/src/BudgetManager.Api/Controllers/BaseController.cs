using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMapper Mapper;
    protected readonly ISender Mediator;

    protected BaseController(IMapper mapper, ISender mediator)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}
