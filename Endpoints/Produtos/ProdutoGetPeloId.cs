using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WantApp.Infra.Dados;
using WantApp.Servicos.Produtos;

namespace WantApp.Endpoints.Produtos;

public class ProdutoGetPeloId
{
    public static string Template => "/produtos/{Id:Guid}";
    public static string[] Metodos => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    //[Authorize(Policy = "EmpregadoPolitica")]
    [AllowAnonymous]
    public static async Task<IResult> Action([FromRoute] Guid? Id,ApplicationDbContext context, ProdutoGetServico produtoGetServico)
    {
        List<string> erros = new List<string>();

        if (Id == null)
            erros.Add("Você precisa preencher o parâmetro 'Id'!");

        if (erros.Count > 0)
            return Results.ValidationProblem(erros.ToArray().ConverterParaProblemaDetalhado());        

        return Results.Ok(produtoGetServico.BuscarPeloId(Id.Value));
    }
}
