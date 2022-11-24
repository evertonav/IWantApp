using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Produtos;

public class ProdutoGetVitrine
{
    public static string Template => "/produtos/vitrine";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ApplicationDbContext context, int pagina = 1, int linhas = 1, string ordenarPor = "Nome")
    {
        if (linhas > 50)
            return Results.Problem(title: "O número de linhas não pode passar de 50!", statusCode: 400);

        //Criar uma classe de serviço para cuidar dessa parte
        var consultaBase = context.Produtos
                            .AsNoTracking() //isso aqui deixa a consulta mais rápido, pois não fica rastreavel
                            .Include(p => p.Categoria)
                            .Where(p => p.TemEstoque && p.Categoria.Ativo);

        if (ordenarPor == "Nome")
            consultaBase = consultaBase.OrderBy(p => p.Nome);
        else if (ordenarPor == "Preco")
            consultaBase = consultaBase.OrderBy(p => p.Preco);
        else
            return Results.Problem(title: "Você precisa preencher com um valor válido no parâmetro 'ordenarPor' com 'Preco' ou 'Nome'!", statusCode: 400);

        var consultaFiltro = consultaBase.Skip((pagina - 1) * linhas).Take(linhas);       

        var produtos = consultaFiltro.ToList();

        var resultado = produtos.Select(p => new ProdutoResponse(p.Id, p.Nome, p.Categoria.Nome, p.Descricao, p.TemEstoque, p.Preco, p.Ativo));
        return Results.Ok(resultado);
    }
}
