using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WantApp.Dominio.Pedidos;
using WantApp.Infra.Dados;
using WantApp.Servicos;
using WantApp.Servicos.Pedidos;
using static System.Net.WebRequestMethods;

namespace WantApp.Endpoints.Pedidos;

public class PedidoGetPeloId
{
    public static string Template => "/pedidos/{id:Guid}";
    public static string[] Metodos => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action([FromRoute] Guid Id, HttpContext http, PedidoGetServico pedidoGetServico, 
        UsuarioServico usuarioServico, IConfiguration configuracao)
    {
        PedidoResponse pedidoResponse = pedidoGetServico.BuscarPeloId(Id);

        List<string> erros = new List<string>();

        if (pedidoResponse == null)        
            erros.Add("Não há pedidos com esse id!");

        if (pedidoResponse != null && (pedidoResponse.ClienteId != usuarioServico.UsuarioLogado(http)))        
        {
            if (usuarioServico.CodigoEmpregado(http) != usuarioServico.CodigoEmpregado(pedidoResponse.ClienteId, configuracao).Valor)
                erros.Add("O usuário logado não pode visualizar esse pedido!");          
        }           

        if (erros.Count > 0)
            return Results.ValidationProblem(erros.ToArray().ConverterParaProblemaDetalhado());

        return Results.Ok(pedidoResponse);    
    }
}
