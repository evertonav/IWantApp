using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WantApp.Infra.Dados;
using WantApp.Servicos.Produtos;

namespace WantApp.Endpoints.Produtos;

public class ProdutoGetTodos
{
    public static string Template => "/produtos";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmpregadoPolitica")]
    public static async Task<IResult> Action(ApplicationDbContext context, ProdutoGetServico produtoGetServico)
    {        
        return Results.Ok(produtoGetServico.BuscarTodos());
    }
}
