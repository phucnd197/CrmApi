using MediatR;
using Microsoft.AspNetCore.Mvc;
using User_Api.Features.GetUserById;

namespace User_Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserByIdQuery(id), cancellationToken));
    }
}
