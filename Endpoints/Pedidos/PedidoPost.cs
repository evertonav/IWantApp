using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Clientes;
using WantApp.Infra.Dados;
using WantApp.Dominio.Pedidos;
using WantApp.Servicos.Pedidos;

namespace WantApp.Endpoints.Pedidos;

public class PedidoPost
{
    public static string Template => "/pedidos";
    public static string[] Metodos => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CPFPolitica")]
    public static async Task<IResult> Action(PedidoRequest pedidoRequest, HttpContext http, PedidoServico pedidoServico)
    {
        var idCliente = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var nomeCliente = http.User.Claims.First(c => c.Type == "Nome").Value;

        Pedido pedido = await pedidoServico.AdicionarAsync(idCliente, nomeCliente, pedidoRequest);        

        if (!pedido.IsValid)
            return Results.ValidationProblem(pedido.Notifications.ConverterParaProblemaDetalhado());        

        return Results.Created($"/Pedido/{pedido.Id}", pedido.Id);
    }
}
