using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Produtos;

public class ProdutoGetPeloId
{
    public static string Template => "/produtos/{Id:Guid}";
    public static string[] Metodos => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmpregadoPolitica")]
    public static async Task<IResult> Action([FromRoute] Guid? Id,ApplicationDbContext context)
    {
        List<string> erros = new List<string>();

        if (Id == null)
            erros.Add("Você precisa preencher o parâmetro 'Id'!");

        if (erros.Count > 0)
            return Results.ValidationProblem(erros.ToArray().ConverterParaProblemaDetalhado());

        //Criar uma classe de serviço para cuidar dessa parte
        var produtos = context.Produtos.Include(p => p.Categoria).Where(p => p.Id == Id).OrderBy(p => p.Nome).ToList();
        var resultado = produtos.Select(p => new ProdutoResponse(p.Id, p.Nome, p.Categoria.Nome, p.Descricao, p.TemEstoque, p.Preco, p.Ativo));
        return Results.Ok(resultado);
    }
}
