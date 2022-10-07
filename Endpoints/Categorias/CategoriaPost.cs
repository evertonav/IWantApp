using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPost
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize]
    public static IResult Action(CategoriaRequest categoriaRequest, HttpContext http, ApplicationDbContext context)
    {
        var usuarioId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        Categoria categoria = new Categoria(categoriaRequest.Nome, usuarioId, usuarioId);

        if (!categoria.IsValid)
        {      
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }

        context.Categorias.Add(categoria);
        context.SaveChanges();

        return Results.Created($"/categorias/{categoria.Id}", categoria.Id);
    }
}
