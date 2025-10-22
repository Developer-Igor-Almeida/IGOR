using Microsoft.EntityFrameworkCore;
using TMB_Pedidos.Domain.Orders;
using TMB_Pedidos.Infrastructure.Database;

namespace TMB_Pedidos.Infrastructure.Repositories;

public sealed class OrderRepository(AppDbContext db) : IOrderRepository
{
    public Task AddAsync(Order order, CancellationToken ct = default)
        => db.Orders.AddAsync(order, ct).AsTask();

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken ct = default)
        => await db.Orders.OrderByDescending(o => o.DataCriacao).ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => db.SaveChangesAsync(ct);
}
