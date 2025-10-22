using Microsoft.AspNetCore.Mvc;
using TMB_Pedidos.Application.Orders;

namespace TMB_Pedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService svc) : ControllerBase
{
    // POST /api/orders
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
    {
        if (request is null) return BadRequest("Dados inválidos");
        var id = await svc.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    // GET /api/orders
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) 
        => Ok(await svc.ListAsync(ct));

    // GET /api/orders/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await svc.GetAsync(id, ct);
        return order is null ? NotFound() : Ok(order);
    }

}
