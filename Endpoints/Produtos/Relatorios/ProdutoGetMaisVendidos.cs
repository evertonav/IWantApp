using Microsoft.AspNetCore.Authorization;
using WantApp.Servicos.Produtos.Relatorios;

namespace WantApp.Endpoints.Produtos.Relatorios;

public class ProdutoGetMaisVendidos
{
    public static string Template => "/produtos/relatorio/maisVendidos";
    public static string[] Metodos => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(int? pagina, int? linhas, RelatorioProdutosMaisVendidos relatorioProdutosMaisVendidos)
    {
        List<string> erros = new List<string>();

        if (pagina == null)
            erros.Add("Você precisa preencher o parâmetro 'pagina'!");

        if (linhas == null)
            erros.Add("Você precisa preencher o parâmetro 'linhas'!");

        if (erros.Count > 0)
            return Results.ValidationProblem(erros.ToArray().ConverterParaProblemaDetalhado());

        return Results.Ok(relatorioProdutosMaisVendidos.Executar(pagina.Value, linhas.Value));
    }
}
