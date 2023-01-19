using WantApp.Dominio.Produtos;

namespace WantApp.Endpoints.Pedidos;

public record PedidoResponse(Guid IdPedido, string ClienteId, List<Produto> Produtos, decimal Total, string EnderecoEntrega);
