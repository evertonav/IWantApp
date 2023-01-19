using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Servicos.Produtos;

public class ProdutoGetServico
{
    private readonly ApplicationDbContext Context;

    public ProdutoGetServico(ApplicationDbContext context)
    {
        Context = context;
    }

    public IEnumerable<ProdutoResponse> BuscarPeloId(Guid id)
    {
        return new ProdutoServico(Context)
                     .BuscarPeloId(id)
                     .Select(p => new ProdutoResponse(p.Id, p.Nome, p.Categoria.Nome, p.Descricao, p.TemEstoque, p.Preco, p.Ativo));
    }

    public IEnumerable<ProdutoResponse> BuscarTodos()
    {
        return new ProdutoServico(Context)
                    .BuscarTodos()
                    .Select(p => new ProdutoResponse(p.Id, p.Nome, p.Categoria.Nome, p.Descricao, p.TemEstoque, p.Preco, p.Ativo));
    }
}
