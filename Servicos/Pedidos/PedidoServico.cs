using WantApp.Dominio.Pedidos;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Pedidos;
using WantApp.Infra.Dados;
using WantApp.Servicos.Produtos;

namespace WantApp.Servicos.Pedidos;

public class PedidoServico
{
    private readonly ApplicationDbContext Context;

    public PedidoServico(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<Pedido> AdicionarAsync(string idCliente, string nomeCliente, PedidoRequest pedidoRequest)
    {
        List<Produto> produtosFiltrados = new ProdutoServico(Context).BuscarGrupo(pedidoRequest.ProdutosIds);

        Pedido pedido = new Pedido(idCliente, nomeCliente, produtosFiltrados, pedidoRequest.EnderecoEntrega);

        if (!pedido.IsValid)
            return pedido;

        Context.Pedido.Add(pedido);
        await Context.SaveChangesAsync();

        return pedido;
    }
}
