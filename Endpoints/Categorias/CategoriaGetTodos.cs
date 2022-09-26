using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Categorias;

public class CategoriaGetTodos
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(ApplicationDbContext context)
    {
        List<Categoria> categorias = context.Categorias.ToList();
        var response = categorias.Select(c => new CategoriaResponse { Id = c.Id, Nome = c.Nome, Ativo = c.Ativo });
        
        return Results.Ok(response);
    }
}
