namespace TMB_Pedidos.Domain.Orders;

public enum OrderStatus { Pendente = 0, Processando = 1, Finalizado = 2 }

public sealed class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Cliente { get; private set; } = default!;
    public string Produto { get; private set; } = default!;
    public decimal Valor { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pendente;
    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;

    private Order() { } // EF

    public Order(string cliente, string produto, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(cliente)) throw new ArgumentException("Cliente requerido");
        if (string.IsNullOrWhiteSpace(produto)) throw new ArgumentException("Produto requerido");
        if (valor < 0) throw new ArgumentException("Valor inválido");

        Cliente = cliente.Trim();
        Produto = produto.Trim();
        Valor = valor;
        Status = OrderStatus.Pendente;
        DataCriacao = DateTime.UtcNow;
    }
}
