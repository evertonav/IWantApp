using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPut
{
    public static string Template => "/categorias/{id:Guid}";
    public static string[] Metodos => new string[] {HttpMethod.Put.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id,
        HttpContext http, CategoriaRequest categoriaRequest, CategoriaServico categoriaServico)//ApplicationDbContext context)
    {
        string usuarioId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        Categoria categoria = categoriaServico.Atualizar(categoriaServico.BuscarPeloId(Id),
                                                         categoriaRequest,
                                                         usuarioId);                         

        if (categoria == null)
        {
            return Results.NotFound();
        }        

        if (!categoria.IsValid)
        {
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }                

        return Results.Ok();
    }


}
