using Microsoft.EntityFrameworkCore;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Pedidos;
using WantApp.Infra.Dados;

namespace WantApp.Servicos.Produtos;

public class ProdutoServico
{
    private readonly ApplicationDbContext Context;

    public ProdutoServico(ApplicationDbContext context)
    {
        Context = context;
    }

    public List<Produto> BuscarGrupo(List<Guid> idsProdutos)
    {
        if (idsProdutos != null || idsProdutos.Any())
            return Context.Produtos.Where(p => idsProdutos.Contains(p.Id)).ToList();

        return null;
    }

    public List<Produto> BuscarPeloId(Guid id)
    {
        return Context.Produtos.Include(p => p.Categoria).Where(p => p.Id == id).OrderBy(p => p.Nome).ToList();
    }

    public List<Produto> BuscarTodos()
    {
        return Context.Produtos.Include(p => p.Categoria).OrderBy(p => p.Nome).ToList();
    }
}
