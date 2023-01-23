using WantApp.Dominio.Produtos;

namespace WantApp.Endpoints.Pedidos;

public record PedidoResponseMeu(Guid IdPedido, string ClienteId, List<Produto> Produtos, decimal Total, string EnderecoEntrega);
