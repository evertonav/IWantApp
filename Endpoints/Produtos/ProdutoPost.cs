using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos.Usuarios;
using static System.Net.WebRequestMethods;

namespace WantApp.Endpoints.Produtos;

public class ProdutoPost
{
    public static string Template => "/produtos";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmpregadoPolitica")]
    public static async Task<IResult> Action(ProdutoRequest produtoRequest, 
        HttpContext http, ApplicationDbContext context)
    {        
        var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == produtoRequest.CategoriaId);

        var produto = new Produto(produtoRequest.Nome,
                                  categoria,
                                  produtoRequest.Descricao,
                                  produtoRequest.TemEstoque,
                                  produtoRequest.preco,
                                  produtoRequest.Ativo,
                                  new InformacoesTokenServico(http).UsuarioLogado());

        if (!produto.IsValid)
        {
            return Results.ValidationProblem(produto.Notifications.ConverterParaProblemaDetalhado());
        }

        await context.Produtos.AddAsync(produto);
        await context.SaveChangesAsync(); 

        return Results.Created($"/produtos/{produto.Id}", produto.Id);
    }
}
