using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints;
using WantApp.Endpoints.Categorias;
using WantApp.Infra.Dados;
using static System.Net.WebRequestMethods;

namespace WantApp.Servicos;

public class CategoriaServico
{
    private readonly ApplicationDbContext Context;

    public CategoriaServico(ApplicationDbContext context)
    {
        Context = context;
    }

    public List<Categoria> BuscarTodas()
    {
        return Context.Categorias.ToList();
    }

    public IEnumerable<CategoriaResponse> BuscarTodasResponse()
    {
        return BuscarTodas().Select(c => new CategoriaResponse { Id = c.Id, Nome = c.Nome, Ativo = c.Ativo });
    }

    public async Task<Categoria> AdicionarAsync(string idUsuario, CategoriaRequest categoriaRequest)
    {
        Categoria categoria = new Categoria(categoriaRequest.Nome, idUsuario, idUsuario);

        if (!categoria.IsValid)
        {
            return categoria;
        }

        await Context.Categorias.AddAsync(categoria);
        await Context.SaveChangesAsync();

        return categoria;
    }

    public Categoria BuscarPeloId(Guid idCategoria)
    {
        return Context.Categorias.FirstOrDefault(x => x.Id == idCategoria);
    }

    public Categoria Atualizar(Categoria categoria, CategoriaRequest categoriaRequest, string idUsuario)
    {
        if (categoria == null)
            return null;               

        categoria.EditarInformacoes(categoriaRequest.Nome, categoriaRequest.Ativo, idUsuario);

        if (!categoria.IsValid)
        {
            return categoria;
        }

        Context.SaveChanges();

        return categoria;
    }
}
