using Microsoft.EntityFrameworkCore;
using TMB_Pedidos.Domain.Orders;

namespace TMB_Pedidos.Infrastructure.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Order>(e =>
        {
            e.ToTable("orders");
            e.HasKey(x => x.Id);
            e.Property(x => x.Cliente).HasMaxLength(200).IsRequired();
            e.Property(x => x.Produto).HasMaxLength(200).IsRequired();
            e.Property(x => x.Valor).HasColumnType("numeric(18,2)").IsRequired();
            e.Property(x => x.Status).HasConversion<int>().IsRequired();
            e.Property(x => x.DataCriacao).IsRequired();
            e.HasIndex(x => x.DataCriacao);
        });
    }
}
