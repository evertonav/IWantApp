using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos;
using WantApp.Servicos.Usuarios;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPost
{
    public static string Template => "/categorias";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(CategoriaRequest categoriaRequest, 
        HttpContext http, CategoriaServico categoriaServico)
    {       
        Categoria categoria = await categoriaServico.AdicionarAsync(new InformacoesTokenServico(http).UsuarioLogado(),
                                                                    categoriaRequest);        

        if (!categoria.IsValid)
        {      
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }        

        return Results.Created($"/categorias/{categoria.Id}", categoria.Id);
    }
}
