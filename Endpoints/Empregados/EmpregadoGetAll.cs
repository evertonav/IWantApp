using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Empregados;

public class EmpregadoGetAll
{
    public static string Template => "/empregados";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [Authorize(Policy = "Empregado005Politica")]
    public static IResult Action(int? pagina, int? linhas, BuscarTodosUsuariosComClaimName buscarTodosUsuariosComClaimName)
    {
        List<string> erros = new List<string>();

        if (pagina == null)      
            erros.Add("Você precisa preencher o parâmetro 'pagina'!");                    

        if (linhas == null)
            erros.Add("Você precisa preencher o parâmetro 'linhas'!");                        

        if (erros.Count > 0)
            return Results.ValidationProblem(erros.ToArray().ConverterParaProblemaDetalhado());

        //Utilizando o Dapper                  
        return Results.Ok(buscarTodosUsuariosComClaimName.Executar(pagina.Value, linhas.Value));
    }
}
