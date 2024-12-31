using MediatR;
using Microsoft.AspNetCore.Mvc;
using PIM_Api.Features.GetAllProducts;
using PIM_Api.Features.GetProductPrices;

namespace PIM_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllProductsQuery(), cancellationToken));
    }

    [HttpGet("{id:int}/prices")]
    public async Task<IActionResult> GetProductPrices(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetProductPricesQuery(id), cancellationToken));
    }
}
