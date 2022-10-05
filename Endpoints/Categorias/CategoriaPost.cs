using Microsoft.AspNetCore.Authorization;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPost
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize]
    public static IResult Action(CategoriaRequest categoriaRequest, ApplicationDbContext context)
    {
        Categoria categoria = new Categoria(categoriaRequest.Nome, "Everton", "Everton");

        if (!categoria.IsValid)
        {      
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }

        context.Categorias.Add(categoria);
        context.SaveChanges();

        return Results.Created($"/categorias/{categoria.Id}", categoria.Id);
    }
}
