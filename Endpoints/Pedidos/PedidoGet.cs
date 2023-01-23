using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WantApp.Servicos.Pedidos;
using WantApp.Servicos;
using WantApp.Infra.Dados;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WantApp.Endpoints.Pedidos;

public class PedidoGet
{
    public static string Template => "/pedidos/{id:Guid}";
    public static string[] Metodos => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    //Essa classe é o desafio 1 do curso, solução feita pelo professor.
    [Authorize]
    public static async Task<IResult> Action([FromRoute] Guid Id, HttpContext http, ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        var clienteClaim = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var codigoEmpregadoClaim = http.User.Claims.FirstOrDefault(c => c.Type == "CodigoEmpregado");

        var pedido = context.Pedido.Include(p => p.Produtos).FirstOrDefault(p => p.Id == Id);

        if (pedido.ClienteId != clienteClaim.Value && codigoEmpregadoClaim == null)
            return Results.Forbid();

        var cliente = await userManager.FindByIdAsync(pedido.ClienteId);

        var produtoResponse = pedido.Produtos.Select(p => new PedidoProduto(p.Id, p.Nome));
        var pedidoResponse = new PedidoResponse(pedido.Id, cliente.Email, produtoResponse, pedido.EnderecoEntrega);

        return Results.Ok(pedidoResponse);
    }
}
