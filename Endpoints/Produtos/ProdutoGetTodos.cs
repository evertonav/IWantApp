using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Produtos;

public class ProdutoGetTodos
{
    public static string Template => "/produtos";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmpregadoPolitica")]
    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        //Criar uma classe de serviço para cuidar dessa parte
        var produtos = context.Produtos.Include(p => p.Categoria).OrderBy(p => p.Nome).ToList();
        var resultado = produtos.Select(p => new ProdutoResponse(p.Id, p.Nome, p.Categoria.Nome, p.Descricao, p.TemEstoque, p.Preco, p.Ativo));
        return Results.Ok(resultado);
    }
}
