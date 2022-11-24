using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPost
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(CategoriaRequest categoriaRequest, 
        HttpContext http, ApplicationDbContext context, CategoriaServico categoriaServico)
    {

        var usuarioId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        Categoria categoria = await categoriaServico.AdicionarAsync(usuarioId,
                                                                    categoriaRequest);        

        if (!categoria.IsValid)
        {      
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }        

        return Results.Created($"/categorias/{categoria.Id}", categoria.Id);
    }
}
