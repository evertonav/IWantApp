namespace WantApp.Endpoints.Pedidos;

public record PedidoResponse(Guid Id, string EmailCliente, IEnumerable<PedidoProduto> Produtos, string EnderecoEntrega);

public record PedidoProduto(Guid Id, string Nome);