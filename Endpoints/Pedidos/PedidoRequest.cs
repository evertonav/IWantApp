namespace WantApp.Endpoints.Pedidos;

public record PedidoRequest(List<Guid> ProdutosIds, string EnderecoEntrega);
