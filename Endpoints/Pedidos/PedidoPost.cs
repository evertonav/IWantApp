using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Endpoints.Clientes;
using WantApp.Infra.Dados;
using WantApp.Dominio.Pedidos;

namespace WantApp.Endpoints.Pedidos;

public class PedidoPost
{
    public static string Template => "/pedidos";
    public static string[] Metodos => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CPFPolitica")]
    public static async Task<IResult> Action(PedidoRequest pedidoRequest, HttpContext http, ApplicationDbContext context)
    {
        var idCliente = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var nomeCliente = http.User.Claims.First(c => c.Type == "Nome").Value;

        //        if (pedidoRequest.ProdutosIds == null || !pedidoRequest.ProdutosIds.Any())
        //            return Results.BadRequest("Produto é obrigatório para pedido!");

        //        if (string.IsNullOrEmpty(pedidoRequest.EnderecoEntrega))
        //            return Results.BadRequest("Endereço de entrega é obrigatório!");

        List<Produto> produtosFiltrados = null;

        if (pedidoRequest.ProdutosIds != null || pedidoRequest.ProdutosIds.Any())
            produtosFiltrados = context.Produtos.Where(p => pedidoRequest.ProdutosIds.Contains(p.Id)).ToList();

        Pedido pedido = new Pedido(idCliente, nomeCliente, produtosFiltrados, pedidoRequest.EnderecoEntrega);

        if (!pedido.IsValid)
            return Results.ValidationProblem(pedido.Notifications.ConverterParaProblemaDetalhado());

        context.Pedido.Add(pedido);
        await context.SaveChangesAsync();

        return Results.Created($"/Pedido/{pedido.Id}", pedido.Id);
    }
}
