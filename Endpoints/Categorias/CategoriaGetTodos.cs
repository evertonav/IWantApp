using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos;

namespace WantApp.Endpoints.Categorias;

public class CategoriaGetTodos
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(ApplicationDbContext context, CategoriaServico categoriaServico)
    {               
        return Results.Ok(categoriaServico.BuscarTodasResponse());
    }
}
