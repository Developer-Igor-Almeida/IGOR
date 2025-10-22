using TMB_Pedidos.Domain.Orders;

namespace TMB_Pedidos.Application.Orders;

public interface IOrderService
{
    Task<Guid> CreateAsync(CreateOrderRequest req, CancellationToken ct = default);
    Task<IReadOnlyList<OrderResponse>> ListAsync(CancellationToken ct = default);
    Task<OrderResponse?> GetAsync(Guid id, CancellationToken ct = default);
}

public sealed class OrderService(IOrderRepository repo) : IOrderService
{
    public async Task<Guid> CreateAsync(CreateOrderRequest req, CancellationToken ct = default)
    {
        var order = new Order(req.Cliente, req.Produto, req.Valor);
        await repo.AddAsync(order, ct);
        await repo.SaveChangesAsync(ct);
        return order.Id;
    }

    public async Task<IReadOnlyList<OrderResponse>> ListAsync(CancellationToken ct = default) 
        => (await repo.GetAllAsync(ct)).Select(ToResponse).ToList();

    public async Task<OrderResponse?> GetAsync(Guid id, CancellationToken ct = default) 
        => (await repo.GetByIdAsync(id, ct)) is { } o ? ToResponse(o) : null;

    private static OrderResponse ToResponse(Order o) => new(o.Id, o.Cliente, o.Produto, o.Valor, o.Status.ToString(), o.DataCriacao);
}
