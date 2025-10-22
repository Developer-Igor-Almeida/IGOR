namespace TMB_Pedidos.Application.Orders;

public record CreateOrderRequest(string Cliente, string Produto, decimal Valor);
public record OrderResponse(Guid Id, string Cliente, string Produto, decimal Valor, string Status, DateTime DataCriacao);
