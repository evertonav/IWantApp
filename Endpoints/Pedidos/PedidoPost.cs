using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Clientes;
using WantApp.Infra.Dados;
using WantApp.Dominio.Pedidos;
using WantApp.Servicos.Pedidos;
using WantApp.Servicos.Usuarios;

namespace WantApp.Endpoints.Pedidos;

public class PedidoPost
{
    public static string Template => "/pedidos";
    public static string[] Metodos => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CPFPolitica")]
    public static async Task<IResult> Action(PedidoRequest pedidoRequest, HttpContext http, PedidoServico pedidoServico)
    {
        InformacoesTokenServico informacoesTokenServico = new InformacoesTokenServico(http);        

        Pedido pedido = await pedidoServico.AdicionarAsync(informacoesTokenServico.UsuarioLogado(),
                                                           informacoesTokenServico.NomeUsuario(), 
                                                           pedidoRequest);        

        if (!pedido.IsValid)
            return Results.ValidationProblem(pedido.Notifications.ConverterParaProblemaDetalhado());        

        return Results.Created($"/Pedido/{pedido.Id}", pedido.Id);
    }
}
