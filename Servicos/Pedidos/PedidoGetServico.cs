using Microsoft.EntityFrameworkCore;
using System.Collections;
using WantApp.Endpoints.Pedidos;
using WantApp.Infra.Dados;

namespace WantApp.Servicos.Pedidos;

public class PedidoGetServico
{
    private readonly ApplicationDbContext _context;

    public PedidoGetServico(ApplicationDbContext context)
    {
        _context = context;
    }

    public PedidoResponse BuscarPeloId(Guid id)
    {
        return null;/*_context
                 .Pedido
                 .Where(X => X.Id == id )
                 .Select(p => new PedidoResponse(p.Id, p.ClienteId, p.Produtos, p.Total, p.EnderecoEntrega))
                 .FirstOrDefault();*/
    }
}
